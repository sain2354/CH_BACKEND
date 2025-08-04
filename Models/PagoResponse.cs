namespace CH_BACKEND.Models
{
    public class PagoResponse
    {
        public int IdPago { get; set; }
        public int IdVenta { get; set; }
        public decimal MontoPagado { get; set; }
        public DateOnly FechaPago { get; set; }
        public int? IdMedioPago { get; set; }
        public string? PreferenceIdMP { get; set; }
        public string? IdTransaccionMP { get; set; }
        public string EstadoPago { get; set; } = null!;
        public string? ComprobanteUrl { get; set; }
    }
}
