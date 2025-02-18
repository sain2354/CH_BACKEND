using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

public partial class Venta
{
    [Key]
    public int IdVenta { get; set; }

    public int IdPersona { get; set; }

    [StringLength(50)]
    public string? TipoComprobante { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Total { get; set; }

    [StringLength(50)]
    public string Estado { get; set; } = null!;

    [Column("serie")]
    [StringLength(20)]
    public string? Serie { get; set; }

    [Column("numeroComprobante")]
    [StringLength(20)]
    public string? NumeroComprobante { get; set; }

    [Column("total_igv", TypeName = "decimal(18, 2)")]
    public decimal? TotalIgv { get; set; }

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<Devolucion> Devoluciones { get; set; } = new List<Devolucion>();

    [ForeignKey("IdPersona")]
    [InverseProperty("Venta")]
    public virtual Persona IdPersonaNavigation { get; set; } = null!;

    [InverseProperty("IdVentaNavigation")]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
