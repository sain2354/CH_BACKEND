using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CH_BACKEND.DBCalzadosHuancayo;

[Table("Producto")]
public partial class Producto
{
    [Key]
    [Column("id_producto")]
    public int IdProducto { get; set; }

    [Column("id_categoria")]
    public int IdCategoria { get; set; }

    [Column("id_sub_categoria")]
    public int? IdSubCategoria { get; set; }

    [Column("codigo_barra")]
    [StringLength(50)]
    [Unicode(false)]
    public string CodigoBarra { get; set; } = null!;

    [Column("nombre")]
    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    [Column("stock", TypeName = "decimal(8, 2)")]
    public decimal Stock { get; set; }

    [Column("stock_minimo", TypeName = "decimal(8, 2)")]
    public decimal StockMinimo { get; set; }

    [Column("precio_venta", TypeName = "decimal(8, 2)")]
    public decimal PrecioVenta { get; set; }

    [Column("precio_compra", TypeName = "decimal(8, 2)")]
    public decimal? PrecioCompra { get; set; }

    [Column("id_unidad_medida")]
    public int IdUnidadMedida { get; set; }

    [Column("estado")]
    public bool Estado { get; set; }

    [Column("foto")]
    [Unicode(false)]
    public string? Foto { get; set; }

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<CarritoDetalle> CarritoDetalles { get; set; } = new List<CarritoDetalle>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<Devolucion> Devoluciones { get; set; } = new List<Devolucion>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<HistorialInventario> HistorialInventarios { get; set; } = new List<HistorialInventario>();

    [ForeignKey("IdCategoria")]
    [InverseProperty("Productos")]
    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    [ForeignKey("IdSubCategoria")]
    [InverseProperty("Productos")]
    public virtual SubCategoria? IdSubCategoriaNavigation { get; set; }

    [ForeignKey("IdUnidadMedida")]
    [InverseProperty("Productos")]
    public virtual UnidadMedida IdUnidadMedidaNavigation { get; set; } = null!;

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    [InverseProperty("IdProductoNavigation")]
    public virtual ICollection<TallaProducto> TallaProductos { get; set; } = new List<TallaProducto>();

    [ForeignKey("IdProducto")]
    [InverseProperty("IdProductos")]
    public virtual ICollection<Promocion> IdPromocions { get; set; } = new List<Promocion>();
}
