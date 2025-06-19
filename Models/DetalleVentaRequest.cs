namespace CH_BACKEND.Models
{
    public class DetalleVentaRequest
    {
        public int IdVenta { get; set; }       // <- Lo necesitas para asociar el detalle a la venta
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Total { get; set; }
        public int? IdUnidadMedida { get; set; }
        public decimal? Igv { get; set; }
    }
}
