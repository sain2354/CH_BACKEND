using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

public partial class Devolucion
{
    [Key]
    [Column("id_devolucion")]
    public int IdDevolucion { get; set; }

    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad_devolucion", TypeName = "decimal(8, 2)")]
    public decimal CantidadDevolucion { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("motivo")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Motivo { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("Devoluciones")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [ForeignKey("IdVenta")]
    [InverseProperty("Devoluciones")]
    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
