// Pago.cs (Entidad)
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CH_BACKEND.DBCalzadosHuancayo
{
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
        public int? IdMedioPago { get; set; }

        [Column("preference_id_mp")]
        [StringLength(100)]
        public string? PreferenceIdMP { get; set; }

        [Column("id_transaccion_mp")]
        [StringLength(100)]
        public string? IdTransaccionMP { get; set; }

        [Column("estado_pago", TypeName = "nvarchar(50)")]
        public string EstadoPago { get; set; } = "Pendiente";

        [Column("comprobante_url")]
        [StringLength(500)]
        public string? ComprobanteUrl { get; set; }

        [ForeignKey("IdVenta")]
        [InverseProperty("Pagos")]
        public virtual Venta IdVentaNavigation { get; set; } = null!;
    }
}
