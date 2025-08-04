using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Models;
using CH_BACKEND.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CH_BACKEND.Logica
{
    public class PagoLogica
    {
        private readonly PagoRepository _pagoRepository;

        public PagoLogica(PagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public List<PagoResponse> ObtenerPagos()
        {
            return _pagoRepository.ObtenerTodos()
                .Select(p => new PagoResponse
                {
                    IdPago = p.IdPago,
                    IdVenta = p.IdVenta,
                    MontoPagado = p.MontoPagado,
                    FechaPago = p.FechaPago,
                    IdMedioPago = p.IdMedioPago,
                    PreferenceIdMP = p.PreferenceIdMP,
                    IdTransaccionMP = p.IdTransaccionMP,
                    EstadoPago = p.EstadoPago,
                    ComprobanteUrl = p.ComprobanteUrl
                })
                .ToList();
        }

        public PagoResponse? ObtenerPagoPorId(int id)
        {
            var p = _pagoRepository.ObtenerPorId(id);
            if (p == null) return null;
            return new PagoResponse
            {
                IdPago = p.IdPago,
                IdVenta = p.IdVenta,
                MontoPagado = p.MontoPagado,
                FechaPago = p.FechaPago,
                IdMedioPago = p.IdMedioPago,
                PreferenceIdMP = p.PreferenceIdMP,
                IdTransaccionMP = p.IdTransaccionMP,
                EstadoPago = p.EstadoPago,
                ComprobanteUrl = p.ComprobanteUrl
            };
        }

        public void CrearPago(int idVenta, PagoRequest req)
        {
            var pago = new Pago
            {
                IdVenta = idVenta,
                MontoPagado = req.MontoPagado,
                FechaPago = req.FechaPago,
                IdMedioPago = req.IdMedioPago,
                IdTransaccionMP = req.IdTransaccionMP,
                EstadoPago = string.IsNullOrEmpty(req.EstadoPago) ? "Pendiente" : req.EstadoPago,
                ComprobanteUrl = req.ComprobanteUrl
            };
            _pagoRepository.Agregar(pago);
        }

        public void ActualizarPago(int idPago, PagoRequest req)
        {
            var pago = _pagoRepository.ObtenerPorId(idPago)
                       ?? throw new Exception("Pago no encontrado");
            pago.MontoPagado = req.MontoPagado;
            pago.FechaPago = req.FechaPago;
            pago.IdMedioPago = req.IdMedioPago;
            pago.IdTransaccionMP = req.IdTransaccionMP;
            pago.EstadoPago = string.IsNullOrEmpty(req.EstadoPago) ? pago.EstadoPago : req.EstadoPago;
            pago.ComprobanteUrl = req.ComprobanteUrl;
            _pagoRepository.Actualizar(pago);
        }

        public void EliminarPago(int idPago)
        {
            var pago = _pagoRepository.ObtenerPorId(idPago)
                       ?? throw new Exception("Pago no encontrado");
            _pagoRepository.Eliminar(pago);
        }

        public bool CambiarEstadoPago(int idPago, string nuevoEstado)
        {
            var pago = _pagoRepository.ObtenerPorId(idPago);
            if (pago == null) return false;
            pago.EstadoPago = nuevoEstado;
            _pagoRepository.Actualizar(pago);
            return true;
        }
    }
}
