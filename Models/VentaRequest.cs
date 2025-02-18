namespace CH_BACKEND.Models
{
    public class VentaRequest
    {
        public int IdPersona { get; set; }
        public string? TipoComprobante { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;
        public string? Serie { get; set; }
        public string? NumeroComprobante { get; set; }
        public decimal? TotalIgv { get; set; }
    }

}