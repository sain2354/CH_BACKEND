using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;

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
            var pagos = _pagoRepository.ObtenerTodos();
            return pagos.Select(p => new PagoResponse
            {
                IdPago = p.IdPago,
                IdVenta = p.IdVenta,
                MontoPagado = p.MontoPagado,
                FechaPago = p.FechaPago,
                IdMedioPago = p.IdMedioPago
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
                IdMedioPago = pago.IdMedioPago
            };
        }

        public void CrearPago(PagoRequest request)
        {
            var pago = new Pago
            {
                IdVenta = request.IdVenta,
                MontoPagado = request.MontoPagado,
                FechaPago = request.FechaPago,
                IdMedioPago = request.IdMedioPago
            };
            _pagoRepository.Agregar(pago);
        }
    }
}
