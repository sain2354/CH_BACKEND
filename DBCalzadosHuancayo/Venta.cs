using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo
{
    [Table("Venta")]
    public partial class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        // Se usa IdUsuario para vincular al cliente registrado
        public int IdUsuario { get; set; }

        [StringLength(50)]
        public string? TipoComprobante { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }

        // Se almacena el total de productos y el costo de envío (o la suma de ambos, según convenga)
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }

        [StringLength(50)]
        public string Estado { get; set; } = null!;

        [Column("serie")]
        [StringLength(20)]
        public string? Serie { get; set; }

        [Column("numeroComprobante")]
        [StringLength(20)]
        public string? NumeroComprobante { get; set; }

        [Column("total_igv", TypeName = "decimal(18, 2)")]
        public decimal? TotalIgv { get; set; }

        // Estado del pago de la venta (Pendiente, Pagado, etc.)
        [StringLength(50)]
        public string EstadoPago { get; set; } = "Pendiente";

        // NUEVO: Costo de envío
        [Column("costo_envio", TypeName = "decimal(18, 2)")]
        public decimal CostoEnvio { get; set; }

        // Relaciones con detalle, dirección, pagos, etc.
        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<Devolucion> Devoluciones { get; set; } = new List<Devolucion>();

        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<DireccionEntrega> DireccionEntregas { get; set; } = new List<DireccionEntrega>();

        // Relación con la tabla de usuario (asegúrate de tener la entidad Usuario definida)
        [ForeignKey("IdUsuario")]
        [InverseProperty("Ventas")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

        [InverseProperty("IdVentaNavigation")]
        public virtual ICollection<VentaEstadoHistorial> VentaEstadoHistorials { get; set; } = new List<VentaEstadoHistorial>();
    }
}
