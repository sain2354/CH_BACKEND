using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[PrimaryKey("IdProducto", "IdTalla")]
[Table("Talla_Producto")]
public partial class TallaProducto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Key]
    [Column("id_talla")]
    public int IdTalla { get; set; }

    [Column("stock", TypeName = "decimal(8, 2)")]
    public decimal Stock { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("TallaProductos")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [ForeignKey("IdTalla")]
    [InverseProperty("TallaProductos")]
    public virtual Talla IdTallaNavigation { get; set; } = null!;
}
