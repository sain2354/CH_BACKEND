using System;

namespace CH_BACKEND.Models
{
    public class PagoRequest
    {
        public int IdVenta { get; set; }
        public decimal MontoPagado { get; set; }
        public DateOnly FechaPago { get; set; }
        public int IdMedioPago { get; set; }
    }
}