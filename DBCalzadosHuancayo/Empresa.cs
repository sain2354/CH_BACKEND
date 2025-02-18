using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Empresa")]
public partial class Empresa
{
    [Key]
    [Column("id_empresa")]
    public int IdEmpresa { get; set; }

    [Column("nombre_empresa")]
    [StringLength(100)]
    [Unicode(false)]
    public string NombreEmpresa { get; set; } = null!;

    [Column("ruc")]
    [StringLength(11)]
    [Unicode(false)]
    public string Ruc { get; set; } = null!;

    [Column("direccion")]
    [StringLength(150)]
    [Unicode(false)]
    public string? Direccion { get; set; }

    [Column("telefono")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Telefono { get; set; }

    [Column("correo")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Correo { get; set; }
}
