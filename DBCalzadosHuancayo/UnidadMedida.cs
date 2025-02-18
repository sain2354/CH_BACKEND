using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

public partial class UnidadMedida
{
    [Key]
    [Column("id_unidad_medida")]
    public int IdUnidadMedida { get; set; }

    [Column("descripcion")]
    [StringLength(50)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [InverseProperty("IdUnidadMedidaNavigation")]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    [InverseProperty("IdUnidadMedidaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
