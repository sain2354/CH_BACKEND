using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[PrimaryKey("IdCarrito", "IdProducto", "IdTalla")]
[Table("Carrito_Detalle")]
public partial class CarritoDetalle
{
    [Key]
    [Column("id_carrito")]
    public int IdCarrito { get; set; }

    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad", TypeName = "decimal(8, 2)")]
    public decimal Cantidad { get; set; }

    [Key]
    [Column("id_talla")]
    public int IdTalla { get; set; }

    [ForeignKey("IdCarrito")]
    [InverseProperty("CarritoDetalles")]
    public virtual Carrito IdCarritoNavigation { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("CarritoDetalles")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [ForeignKey("IdTalla")]
    [InverseProperty("CarritoDetalles")]
    public virtual Talla IdTallaNavigation { get; set; } = null!;
}
