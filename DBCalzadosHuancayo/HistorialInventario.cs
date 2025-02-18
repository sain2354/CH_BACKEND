using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("HistorialInventario")]
public partial class HistorialInventario
{
    [Key]
    [Column("id_movimiento")]
    public int IdMovimiento { get; set; }

    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("tipo_movimiento")]
    [StringLength(20)]
    [Unicode(false)]
    public string TipoMovimiento { get; set; } = null!;

    [Column("cantidad", TypeName = "decimal(8, 2)")]
    public decimal Cantidad { get; set; }

    [Column("fecha", TypeName = "datetime")]
    public DateTime Fecha { get; set; }

    [Column("usuario_responsable")]
    public int? UsuarioResponsable { get; set; }

    [Column("documento_compra")]
    [StringLength(50)]
    [Unicode(false)]
    public string? DocumentoCompra { get; set; }

    [Column("id_proveedor")]
    public int? IdProveedor { get; set; }

    [Column("proveedor_nombre")]
    [StringLength(255)]
    public string? ProveedorNombre { get; set; }

    [Column("proveedor_contacto")]
    [StringLength(255)]
    public string? ProveedorContacto { get; set; }

    [Column("compra_total", TypeName = "decimal(8, 2)")]
    public decimal? CompraTotal { get; set; }

    [Column("compra_fecha")]
    public DateOnly? CompraFecha { get; set; }

    [ForeignKey("IdProducto")]
    [InverseProperty("HistorialInventarios")]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [ForeignKey("UsuarioResponsable")]
    [InverseProperty("HistorialInventarios")]
    public virtual Usuario? UsuarioResponsableNavigation { get; set; }
}
