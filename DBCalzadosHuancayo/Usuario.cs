using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Usuario")]
[Index("Username", Name = "UQ__Usuario__F3DBC5723454555F", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("username")]
    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(50)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("nombre_completo")]
    [StringLength(100)]
    [Unicode(false)]
    public string NombreCompleto { get; set; } = null!;

    [InverseProperty("UsuarioResponsableNavigation")]
    public virtual ICollection<HistorialInventario> HistorialInventarios { get; set; } = new List<HistorialInventario>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();
}
