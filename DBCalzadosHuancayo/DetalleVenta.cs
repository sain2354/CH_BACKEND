using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

public partial class DetalleVenta
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("cantidad", TypeName = "decimal(8, 2)")]
    public decimal Cantidad { get; set; }

    [Column("precio", TypeName = "decimal(8, 2)")]
    public decimal Precio { get; set; }

    [Column("descuento", TypeName = "decimal(8, 2)")]
    public decimal? Descuento { get; set; }

    [Column("total", TypeName = "decimal(8, 2)")]
    public decimal Total { get; set; }

    [Column("id_unidad_medida")]
    public int? IdUnidadMedida { get; set; }

    [Column("igv", TypeName = "decimal(8, 2)")]
    public decimal? Igv { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("DetalleVenta")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [ForeignKey("IdUnidadMedida")]
    [InverseProperty("DetalleVenta")]
    public virtual UnidadMedida? IdUnidadMedidaNavigation { get; set; }

    [ForeignKey("IdVenta")]
    [InverseProperty("DetalleVenta")]
    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
