using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[PrimaryKey("IdUsuario", "IdRol")]
[Table("Usuario_Rol")]
public partial class UsuarioRol
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Key]
    [Column("id_rol")]
    public int IdRol { get; set; }

    [Column("fecha_asignacion")]
    public DateOnly FechaAsignacion { get; set; }

    [ForeignKey("IdRol")]
    [InverseProperty("UsuarioRols")]
    public virtual Rol IdRolNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("UsuarioRols")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
