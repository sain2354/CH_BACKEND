namespace CH_BACKEND.Models
{
    public class DetalleVentaResponse
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Total { get; set; }
        public int? IdUnidadMedida { get; set; }
        public decimal? Igv { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
    }
}
