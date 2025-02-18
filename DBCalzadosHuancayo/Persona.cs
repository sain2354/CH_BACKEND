using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Persona")]
public partial class Persona
{
    [Key]
    public int IdPersona { get; set; }

    [StringLength(255)]
    public string Nombre { get; set; } = null!;

    [StringLength(15)]
    public string? Telefono { get; set; }

    [StringLength(255)]
    public string? Correo { get; set; }

    [StringLength(255)]
    public string? Direccion { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FechaRegistro { get; set; }

    [StringLength(50)]
    public string? TipoPersona { get; set; }

    [Column("tipo_documento")]
    [StringLength(50)]
    public string? TipoDocumento { get; set; }

    [Column("numero_documento")]
    [StringLength(15)]
    public string? NumeroDocumento { get; set; }

    [InverseProperty("IdPersonaNavigation")]
    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
