using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CH_BACKEND.DBCalzadosHuancayo
{
    [Table("Talla_Producto")]
    public partial class TallaProducto
    {
        [Key, Column("id_producto", Order = 0)]
        public int IdProducto { get; set; }

        // — Ahora campos de la talla enriquecida —
        [Key, Column("usa", Order = 1)]
        public int Usa { get; set; }

        [Column("eur")]
        public int Eur { get; set; }

        [Column("cm", TypeName = "decimal(8,2)")]
        public decimal Cm { get; set; }

        [Column("stock", TypeName = "decimal(8, 2)")]
        public decimal Stock { get; set; }

        [ForeignKey("IdProducto")]
        [InverseProperty("TallaProductos")]
        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
