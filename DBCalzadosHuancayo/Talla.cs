using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Talla")]
public partial class Talla
{
    [Key]
    [Column("id_talla")]
    public int IdTalla { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdTallaNavigation")]
    public virtual ICollection<TallaProducto> TallaProductos { get; set; } = new List<TallaProducto>();
}
