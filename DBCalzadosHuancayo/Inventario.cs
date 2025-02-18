using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Inventario")]
public partial class Inventario
{
    [Key]
    [Column("id_inventario")]
    public int IdInventario { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad_inventario", TypeName = "decimal(8, 2)")]
    public decimal CantidadInventario { get; set; }

    [Column("fecha_registro")]
    public DateOnly FechaRegistro { get; set; }

    [Column("tipo_movimiento")]
    [StringLength(20)]
    [Unicode(false)]
    public string TipoMovimiento { get; set; } = null!;

    [ForeignKey("IdProducto")]
    [InverseProperty("Inventarios")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
