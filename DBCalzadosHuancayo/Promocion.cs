using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Promocion")]
public partial class Promocion
{
    [Key]
    [Column("id_promocion")]
    public int IdPromocion { get; set; }

    [Column("descripcion")]
    [StringLength(100)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [Column("fecha_inicio")]
    public DateOnly FechaInicio { get; set; }

    [Column("fecha_fin")]
    public DateOnly FechaFin { get; set; }

    [Column("tipo_descuento")]
    [StringLength(20)]
    [Unicode(false)]
    public string TipoDescuento { get; set; } = null!;

    [Column("descuento", TypeName = "decimal(8, 2)")]
    public decimal Descuento { get; set; }

    [ForeignKey("IdPromocion")]
    [InverseProperty("IdPromocions")]
    public virtual ICollection<Producto> IdProductos { get; set; } = new List<Producto>();
}
