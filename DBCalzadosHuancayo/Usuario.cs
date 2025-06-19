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
    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("nombre_completo")]
    [StringLength(100)]
    [Unicode(false)]
    public string NombreCompleto { get; set; } = null!;

    // NUEVOS CAMPOS:
    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("telefono")]
    [StringLength(20)]
    [Unicode(false)]
    public string Telefono { get; set; } = "No registrado"; // Valor por defecto

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    [InverseProperty("UsuarioResponsableNavigation")]
    public virtual ICollection<HistorialInventario> HistorialInventarios { get; set; } = new List<HistorialInventario>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<UsuarioDireccion> UsuarioDireccions { get; set; } = new List<UsuarioDireccion>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();

    // NUEVO: Propiedad para almacenar las ventas asociadas al usuario
    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
}
