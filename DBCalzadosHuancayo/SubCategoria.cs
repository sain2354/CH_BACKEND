using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

public partial class SubCategoria
{
    [Key]
    [Column("id_sub_categoria")]
    public int IdSubCategoria { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    [ForeignKey("IdCategoria")]
    [InverseProperty("SubCategoria")]
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    [InverseProperty("IdSubCategoriaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
