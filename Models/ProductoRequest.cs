using System.ComponentModel.DataAnnotations;

namespace CH_BACKEND.Models
{
    public class ProductoRequest
    {
        public int? Id { get; set; }  // Puede ser nullable en caso de creación de productos.
        [Required]
        public int IdCategoria { get; set; }
        public int? IdSubCategoria { get; set; }
        [Required]
        public string CodigoBarra { get; set; } = null!;
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = null!;
        [Range(0, int.MaxValue)]
        public decimal Stock { get; set; }
        [Range(0, int.MaxValue)]
        public decimal StockMinimo { get; set; }
        [Range(0, double.MaxValue)]
        public decimal PrecioVenta { get; set; }
        [Range(0, double.MaxValue)]
        public decimal? PrecioCompra { get; set; }
        [Required]
        public int IdUnidadMedida { get; set; }
        public bool Estado { get; set; }
        public string? Foto { get; set; }
    }

}
