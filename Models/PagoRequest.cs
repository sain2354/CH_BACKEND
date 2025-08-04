namespace CH_BACKEND.Models
{
    public class PagoRequest
    {
        public decimal MontoPagado { get; set; }
        public DateOnly FechaPago { get; set; }
        public int IdMedioPago { get; set; }
        public string? IdTransaccionMP { get; set; }
        public string? EstadoPago { get; set; }

        // Obligatorio: la URL del comprobante
        public string ComprobanteUrl { get; set; } = null!;
    }
}
