using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly PagoLogica _pagoLogica;
        private readonly IConfiguration _config;

        public PagoController(PagoLogica pagoLogica, IConfiguration config)
        {
            _pagoLogica = pagoLogica;
            _config = config;
        }

        // GET: api/Pago
        [HttpGet]
        public ActionResult<List<PagoResponse>> ObtenerPagos()
        {
            return Ok(_pagoLogica.ObtenerPagos());
        }

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public ActionResult<PagoResponse> ObtenerPagoPorId(int id)
        {
            var pago = _pagoLogica.ObtenerPagoPorId(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        // POST: api/Pago
        [HttpPost]
        public IActionResult CrearPago([FromBody] PagoRequest request)
        {
            _pagoLogica.CrearPago(request);
            return CreatedAtAction(nameof(ObtenerPagoPorId), new { id = request.IdVenta }, request);
        }

        // ---------------------------------------------------------------------
        // NUEVO: Generar Preferencia en Mercado Pago usando HttpClient
        // POST: api/Pago/GenerarPreferencia
        // Body: { "idVenta": 123 }
        // Devuelve: { preferenceId, initPoint }
        // ---------------------------------------------------------------------
        [HttpPost("GenerarPreferencia")]
        public async Task<IActionResult> GenerarPreferencia([FromBody] GenerarPreferenciaRequest request)
        {
            if (request == null || request.IdVenta <= 0)
            {
                return BadRequest("Debe proporcionar un IdVenta válido.");
            }

            try
            {
                var resultado = await _pagoLogica.GenerarPreferenciaMP(request.IdVenta);
                if (resultado == null)
                {
                    return BadRequest("No se pudo generar la preferencia. Verifica que la Venta exista.");
                }

                // resultado es un tuple (string PreferenceId, string InitPoint)
                return Ok(new
                {
                    preferenceId = resultado.Value.PreferenceId,
                    initPoint = resultado.Value.InitPoint
                });
            }
            catch (System.Exception ex)
            {
                // Devuelve status 500 con mensaje de error y stack trace
                return StatusCode(500, new
                {
                    error = ex.Message,
                    details = ex.StackTrace
                });
            }
        }

        // ---------------------------------------------------------------------
        // NUEVO: Webhook de Mercado Pago
        // POST: api/Pago/Webhook
        // Configura esta URL en tu panel de MP para recibir notificaciones
        // ---------------------------------------------------------------------
        [HttpPost("Webhook")]
        public async Task<IActionResult> Webhook()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(body))
            {
                return BadRequest("No se recibió información del webhook.");
            }

            // Parseamos el JSON
            dynamic data = JsonConvert.DeserializeObject(body);
            if (data == null)
            {
                return BadRequest("No se pudo deserializar la información del webhook.");
            }

            // Normalmente Mercado Pago envía algo como:
            // {
            //   "id": 123456789,
            //   "live_mode": true,
            //   "type": "payment",
            //   "date_created": "...",
            //   "application_id": "...",
            //   "user_id": "...",
            //   "api_version": "...",
            //   "action": "payment.created",
            //   "data": {
            //       "id": "123456789"
            //   }
            // }
            // En "data.id" estaría el Payment ID
            string paymentId = data?.data?.id;
            if (string.IsNullOrEmpty(paymentId))
            {
                // A veces viene en "id" directamente
                paymentId = data?.id;
            }

            // Si no hay paymentId, no podemos continuar
            if (string.IsNullOrEmpty(paymentId))
            {
                return BadRequest("No se encontró paymentId en la notificación.");
            }

            // Llamamos a la lógica para consultar el pago y actualizar estado
            await _pagoLogica.ProcesarPagoAprobado(paymentId);

            return Ok();
        }
    }
}
