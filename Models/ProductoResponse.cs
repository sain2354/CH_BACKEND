using System.Collections.Generic;

namespace CH_BACKEND.Models
{
    public class ProductoResponse
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string CodigoBarra { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Stock { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal? PrecioCompra { get; set; }
        public bool Estado { get; set; }
        public string? Foto { get; set; }
        public int IdCategoria { get; set; }
        public int? IdSubCategoria { get; set; }
        public int IdUnidadMedida { get; set; }

        // ——— Campos originales de características ———
        public string? Mpn { get; set; }
        public string? ShippingInfo { get; set; }
        public string? Material { get; set; }
        public string? Color { get; set; }

        // ——— Nuevos campos para filtrado ———
        public string? Genero { get; set; }
        public string? Articulo { get; set; }
        public string? Estilo { get; set; }

        // — Array de tallas enriquecidas —
        public List<TallaProductoResponse> Sizes { get; set; } = new List<TallaProductoResponse>();

        public ProductoResponse() { }

        public ProductoResponse(DBCalzadosHuancayo.Producto producto)
        {
            IdProducto = producto.IdProducto;
            Nombre = producto.Nombre;
            CodigoBarra = producto.CodigoBarra;
            PrecioVenta = producto.PrecioVenta;
            Stock = producto.Stock;
            StockMinimo = producto.StockMinimo;
            PrecioCompra = producto.PrecioCompra;
            Estado = producto.Estado;
            Foto = producto.Foto;
            IdCategoria = producto.IdCategoria;
            IdSubCategoria = producto.IdSubCategoria;
            IdUnidadMedida = producto.IdUnidadMedida;

            // valores originales
            Mpn = producto.Mpn;
            ShippingInfo = producto.ShippingInfo;
            Material = producto.Material;
            Color = producto.Color;

            // valores para filtrado
            Genero = producto.Genero;
            Articulo = producto.Articulo;
            Estilo = producto.Estilo;

            // tallas
            if (producto.TallaProductos != null)
            {
                foreach (var tp in producto.TallaProductos)
                {
                    Sizes.Add(new TallaProductoResponse
                    {
                        IdProducto = tp.IdProducto,
                        Usa = tp.Usa,
                        Eur = tp.Eur,
                        Cm = tp.Cm,
                        Stock = tp.Stock
                    });
                }
            }
        }
    }
}