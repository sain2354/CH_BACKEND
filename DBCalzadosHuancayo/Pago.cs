using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Pago")]
public partial class Pago
{
    [Key]
    [Column("id_pago")]
    public int IdPago { get; set; }

    [Column("id_venta")]
    public int IdVenta { get; set; }

    [Column("monto_pagado", TypeName = "decimal(8, 2)")]
    public decimal MontoPagado { get; set; }

    [Column("fecha_pago")]
    public DateOnly FechaPago { get; set; }

    [Column("id_medio_pago")]
    public int IdMedioPago { get; set; }

    [ForeignKey("IdMedioPago")]
    [InverseProperty("Pagos")]
    public virtual MedioPago IdMedioPagoNavigation { get; set; } = null!;

    [ForeignKey("IdVenta")]
    [InverseProperty("Pagos")]
    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
