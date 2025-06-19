using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("DireccionEntrega")]
public partial class DireccionEntrega
{
    [Key]
    [Column("id_direccion_entrega")]
    public int IdDireccionEntrega { get; set; }

    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("direccion")]
    [StringLength(255)]
    public string Direccion { get; set; } = null!;

    [Column("lat", TypeName = "decimal(9, 6)")]
    public decimal? Lat { get; set; }

    [Column("lng", TypeName = "decimal(9, 6)")]
    public decimal? Lng { get; set; }

    [Column("referencia")]
    [StringLength(255)]
    public string? Referencia { get; set; }

    [Column("costo_envio", TypeName = "decimal(8, 2)")]
    public decimal CostoEnvio { get; set; }

    [ForeignKey("IdVenta")]
    [InverseProperty("DireccionEntregas")]
    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
