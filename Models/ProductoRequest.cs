namespace CH_BACKEND.Models
{
    public class ProductoRequest
    {
        public int IdCategoria { get; set; }
        public int? IdSubCategoria { get; set; }
        public string CodigoBarra { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public decimal Stock { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal? PrecioCompra { get; set; }
        public int IdUnidadMedida { get; set; }
        public bool Estado { get; set; }
        public string? Foto { get; set; }
    }
}
