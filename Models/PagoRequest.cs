using System;

namespace CH_BACKEND.Models
{
    public class PagoRequest
    {
        public int IdVenta { get; set; }
        public decimal MontoPagado { get; set; }
        public DateOnly FechaPago { get; set; }
        public int IdMedioPago { get; set; }

        // NUEVO: Opcionales, pueden venir desde el frontend si se dispone de ellos.
        public string? IdTransaccionMP { get; set; }
        public string? EstadoPago { get; set; }
    }
}
