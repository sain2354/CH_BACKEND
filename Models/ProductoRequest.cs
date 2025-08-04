using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CH_BACKEND.Models
{
    public class ProductoRequest
    {
        public int? Id { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        public int? IdSubCategoria { get; set; }

        [Required]
        [StringLength(50)]
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

        // ——— Campos originales de características ———
        [MaxLength(100)] public string? Mpn { get; set; }
        public string? ShippingInfo { get; set; }
        [MaxLength(100)] public string? Material { get; set; }
        [MaxLength(50)] public string? Color { get; set; }

        // ——— Nuevos campos para filtrado ———
        [MaxLength(20)] public string? Genero { get; set; }
        [MaxLength(20)] public string? Articulo { get; set; }
        [MaxLength(30)] public string? Estilo { get; set; }

        // — Array de tallas enriquecidas —
        public List<TallaProductoRequest> Sizes { get; set; } = new List<TallaProductoRequest>();

        // ——— Campos para Promoción (Variante INLINE) ———
        // Indica si se desea asignar una promoción al crear/editar
        public bool AsignarPromocion { get; set; }

        // Datos de la promoción (solo si AsignarPromocion == true)
        public PromocionRequest? Promocion { get; set; }
    }
}