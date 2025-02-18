using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Rol")]
public partial class Rol
{
    [Key]
    [Column("id_rol")]
    public int IdRol { get; set; }

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Descripcion { get; set; }

    [InverseProperty("IdRolNavigation")]
    public virtual ICollection<UsuarioRol> UsuarioRols { get; set; } = new List<UsuarioRol>();
}
