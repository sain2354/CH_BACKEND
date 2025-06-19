namespace CH_BACKEND.Models
{
    public class DireccionEntregaRequest
    {
        public int IdVenta { get; set; }
        public string Direccion { get; set; } = null!;
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public string? Referencia { get; set; }
        public decimal CostoEnvio { get; set; }
    }
}
