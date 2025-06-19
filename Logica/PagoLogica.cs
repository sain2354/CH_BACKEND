using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class PagoLogica
    {
        private readonly PagoRepository _pagoRepository;
        private readonly _DbContextCalzadosHuancayo _db;
        private readonly IConfiguration _config;

        public PagoLogica(PagoRepository pagoRepository, _DbContextCalzadosHuancayo db, IConfiguration config)
        {
            _pagoRepository = pagoRepository;
            _db = db;
            _config = config;
        }

        public List<PagoResponse> ObtenerPagos()
        {
            var pagos = _pagoRepository.ObtenerTodos();
            return pagos.Select(p => new PagoResponse
            {
                IdPago = p.IdPago,
                IdVenta = p.IdVenta,
                MontoPagado = p.MontoPagado,
                FechaPago = p.FechaPago,
                EstadoPago = p.EstadoPago,
                IdTransaccionMP = p.IdTransaccionMP
            }).ToList();
        }

        public PagoResponse? ObtenerPagoPorId(int id)
        {
            var pago = _pagoRepository.ObtenerPorId(id);
            if (pago == null) return null;
            return new PagoResponse
            {
                IdPago = pago.IdPago,
                IdVenta = pago.IdVenta,
                MontoPagado = pago.MontoPagado,
                FechaPago = pago.FechaPago,
                EstadoPago = pago.EstadoPago,
                IdTransaccionMP = pago.IdTransaccionMP
            };
        }

        public void CrearPago(PagoRequest request)
        {
            var pago = new Pago
            {
                IdVenta = request.IdVenta,
                MontoPagado = request.MontoPagado,
                FechaPago = request.FechaPago,
                EstadoPago = string.IsNullOrEmpty(request.EstadoPago) ? "Pendiente" : request.EstadoPago,
                IdTransaccionMP = request.IdTransaccionMP
            };
            _pagoRepository.Agregar(pago);
        }

        // Método para generar la preferencia de pago en Mercado Pago (Opción 1: sin shipments)
        public async Task<(string PreferenceId, string InitPoint)?> GenerarPreferenciaMP(int idVenta)
        {
            // 1. Buscar la venta y cargar sus detalles
            var venta = await _db.Ventas
                .Include(v => v.DetalleVenta)
                .FirstOrDefaultAsync(v => v.IdVenta == idVenta);

            if (venta == null)
                return null; // No existe la venta

            if (!venta.DetalleVenta.Any())
                throw new Exception("La venta no tiene detalles. Debes registrar los ítems comprados.");

            var items = venta.DetalleVenta.Select(det => new {
                title = $"Producto {det.IdProducto}",
                quantity = (int)det.Cantidad,
                currency_id = "PEN",
                unit_price = (float)det.Precio
            }).ToList();

            // Opción 1: Se elimina el bloque shipments
            var preferenceData = new
            {
                items = items,
                back_urls = new
                {
                    success = "https://www.mercadopago.com.pe",
                    failure = "https://www.mercadopago.com.pe",
                    pending = "https://www.mercadopago.com.pe"
                },
                auto_return = "approved",
                external_reference = $"VENTA_{idVenta}_{DateTime.Now.Ticks}"
            };

            // Log para verificar el objeto de preferencia
            Console.WriteLine("PreferenceData: " + JsonConvert.SerializeObject(preferenceData));

            var accessToken = _config["MercadoPago:AccessToken"];
            if (string.IsNullOrEmpty(accessToken))
                throw new Exception("AccessToken de Mercado Pago no configurado.");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var content = new StringContent(
                    JsonConvert.SerializeObject(preferenceData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(
                    "https://api.mercadopago.com/checkout/preferences",
                    content
                );

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var mpResponse = JsonConvert.DeserializeObject<JObject>(responseBody);
                    string preferenceId = mpResponse["id"]?.ToString();
                    string initPoint = mpResponse["init_point"]?.ToString();

                    // Registrar un nuevo Pago en la BD con el PreferenceId
                    var pago = new Pago
                    {
                        IdVenta = idVenta,
                        PreferenceIdMP = preferenceId,
                        IdTransaccionMP = null,
                        MontoPagado = 0,
                        FechaPago = DateOnly.FromDateTime(DateTime.Now),
                        EstadoPago = "Pendiente",
                        IdVentaNavigation = venta,
                        IdMedioPago = null
                    };
                    _pagoRepository.Agregar(pago);

                    return (preferenceId, initPoint);
                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al crear preferencia: {errorMsg}");
                }
            }
        }

        public async Task ProcesarPagoAprobado(string paymentId)
        {
            var accessToken = _config["MercadoPago:AccessToken"];
            if (string.IsNullOrEmpty(accessToken))
                throw new Exception("AccessToken de Mercado Pago no configurado.");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);

                var url = $"https://api.mercadopago.com/v1/payments/{paymentId}";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return;

                var body = await response.Content.ReadAsStringAsync();
                dynamic paymentData = JsonConvert.DeserializeObject(body);

                string preferenceId = paymentData.preference_id;
                string status = paymentData.status;
                float? transactionAmount = paymentData.transaction_amount;

                var pago = _db.Pagos
                    .Include(p => p.IdVentaNavigation)
                    .FirstOrDefault(p => p.PreferenceIdMP == preferenceId);

                if (pago == null) return;

                pago.IdTransaccionMP = paymentId;
                pago.MontoPagado = transactionAmount != null ? (decimal)transactionAmount.Value : 0;
                pago.EstadoPago = status == "approved" ? "Pagado" : "Rechazado";

                if (status == "approved")
                {
                    var venta = pago.IdVentaNavigation;
                    if (venta != null)
                    {
                        venta.EstadoPago = "Pagado";
                    }
                }

                await _db.SaveChangesAsync();
            }
        }
    }
}
