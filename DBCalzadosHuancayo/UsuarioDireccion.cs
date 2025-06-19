using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("UsuarioDireccion")]
public partial class UsuarioDireccion
{
    [Key]
    [Column("id_direccion")]
    public int IdDireccion { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

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

    [Column("fecha_registro", TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("UsuarioDireccions")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
