// CH_BACKEND/DBCalzadosHuancayo/Venta.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CH_BACKEND.DBCalzadosHuancayo
{
    [Table("Venta")]
    public partial class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        public int IdUsuario { get; set; }

        [StringLength(50)]
        public string? TipoComprobante { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [StringLength(50)]
        public string Estado { get; set; } = null!;

        [StringLength(20)]
        public string? Serie { get; set; }

        [StringLength(20)]
        public string? NumeroComprobante { get; set; }

        // Estas cuatro reflejan snake_case reales en la BD:
        [Column("total_igv", TypeName = "decimal(18,2)")]
        public decimal? TotalIgv { get; set; }

        [Column("costo_envio", TypeName = "decimal(18,2)")]
        public decimal CostoEnvio { get; set; }

        [Column("metodo_entrega")]
        [StringLength(20)]
        public string MetodoEntrega { get; set; } = "delivery";

        [Column("sucursal_recoge")]
        [StringLength(100)]
        public string? SucursalRecoge { get; set; }

        [StringLength(50)]
        public string EstadoPago { get; set; } = "Pendiente";

        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<DireccionEntrega> DireccionEntregas { get; set; } = new List<DireccionEntrega>();

        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<VentaEstadoHistorial> VentaEstadoHistorials { get; set; } = new List<VentaEstadoHistorial>();

        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<Devolucion> Devoluciones { get; set; } = new List<Devolucion>();

        [ForeignKey("IdUsuario")]
        [InverseProperty("Ventas")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
