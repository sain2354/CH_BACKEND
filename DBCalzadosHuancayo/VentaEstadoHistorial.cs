using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("VentaEstadoHistorial")]
public partial class VentaEstadoHistorial
{
    [Key]
    [Column("id_historial")]
    public int IdHistorial { get; set; }

    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("estado")]
    [StringLength(50)]
    public string Estado { get; set; } = null!;

    [Column("fecha_cambio", TypeName = "datetime")]
    public DateTime FechaCambio { get; set; }

    [ForeignKey("IdVenta")]
    [InverseProperty("VentaEstadoHistorials")]
    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
