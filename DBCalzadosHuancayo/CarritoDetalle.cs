using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo
{
    [PrimaryKey("IdCarrito", "IdProducto", "IdTalla")]
    [Table("Carrito_Detalle")]
    public partial class CarritoDetalle
    {
        [Key, Column("id_carrito", Order = 0)]
        public int IdCarrito { get; set; }

        [Key, Column("id_producto", Order = 1)]
        public int IdProducto { get; set; }

        [Column("cantidad", TypeName = "decimal(8, 2)")]
        public decimal Cantidad { get; set; }

        [Key, Column("id_talla", Order = 2)]
        public int IdTalla { get; set; }

        [ForeignKey("IdCarrito")]
        [InverseProperty("CarritoDetalles")]
        public virtual Carrito IdCarritoNavigation { get; set; } = null!;

        [ForeignKey("IdProducto")]
        [InverseProperty("CarritoDetalles")]
        public virtual Producto IdProductoNavigation { get; set; } = null!;

        // Propiedad eliminada:
        // [ForeignKey("IdTalla")]
        // [InverseProperty("CarritoDetalles")]
        // public virtual Talla IdTallaNavigation { get; set; } = null!;
    }
}
