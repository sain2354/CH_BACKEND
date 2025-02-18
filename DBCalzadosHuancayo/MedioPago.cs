using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("MedioPago")]
public partial class MedioPago
{
    [Key]
    [Column("id_medio_pago")]
    public int IdMedioPago { get; set; }

    [Column("descripcion")]
    [StringLength(30)]
    [Unicode(false)]
    public string Descripcion { get; set; } = null!;

    [Column("titular")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Titular { get; set; }

    [InverseProperty("IdMedioPagoNavigation")]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
