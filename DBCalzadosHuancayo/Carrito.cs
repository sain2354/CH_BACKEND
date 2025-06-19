using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Carrito")]
public partial class Carrito
{
    [Key]
    [Column("id_carrito")]
    public int IdCarrito { get; set; }

    [Column("id_usuario")]
    public int IdUsuario { get; set; }

    [Column("fecha_creacion", TypeName = "datetime")]
    public DateTime FechaCreacion { get; set; }

    [InverseProperty("IdCarritoNavigation")]
    public virtual ICollection<CarritoDetalle> CarritoDetalles { get; set; } = new List<CarritoDetalle>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("Carritos")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
