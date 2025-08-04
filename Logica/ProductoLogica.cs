// ProductoLogica.cs
using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Models;
using CH_BACKEND.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class ProductoLogica
    {
        private readonly ProductoRepositorio _productoRepositorio;
        private readonly TallaProductoRepository _tpRepositorio;

        public ProductoLogica(
            ProductoRepositorio productoRepositorio,
            TallaProductoRepository tpRepositorio)
        {
            _productoRepositorio = productoRepositorio;
            _tpRepositorio = tpRepositorio;
        }

        public async Task<List<ProductoResponse>> ObtenerProductosPorFiltro(
            int cat,
            string? genero,
            string? articulo,
            string? estilo)
        {
            var lista = await _productoRepositorio.ObtenerPorFiltros(cat, genero, articulo, estilo);
            return lista.ConvertAll(p => new ProductoResponse(p));
        }

        public async Task<ProductoResponse?> ObtenerProductoPorId(int id)
        {
            var producto = await _productoRepositorio.ObtenerPorId(id);
            return producto == null ? null : new ProductoResponse(producto);
        }

        public async Task<int> AgregarProducto(ProductoRequest request)
        {
            var producto = new Producto
            {
                IdCategoria = request.IdCategoria,
                IdSubCategoria = request.IdSubCategoria,
                CodigoBarra = request.CodigoBarra,
                Nombre = request.Nombre,
                Stock = request.Stock,
                StockMinimo = request.StockMinimo,
                PrecioVenta = request.PrecioVenta,
                PrecioCompra = request.PrecioCompra,
                IdUnidadMedida = request.IdUnidadMedida,
                Estado = request.Estado,
                Foto = request.Foto,
                Mpn = request.Mpn,
                ShippingInfo = request.ShippingInfo,
                Material = request.Material,
                Color = request.Color,
                Genero = request.Genero,
                Articulo = request.Articulo,
                Estilo = request.Estilo
            };

            await _productoRepositorio.Crear(producto);

            foreach (var size in request.Sizes)
            {
                await _tpRepositorio.CrearTallaProducto(new TallaProducto
                {
                    IdProducto = producto.IdProducto,
                    Usa = size.Usa,
                    Eur = size.Eur,
                    Cm = size.Cm,
                    Stock = size.Stock
                });
            }

            return producto.IdProducto;
        }

        public async Task<bool> ActualizarProducto(int id, ProductoRequest request)
        {
            var existente = await _productoRepositorio.ObtenerPorId(id);
            if (existente == null) return false;

            existente.IdCategoria = request.IdCategoria;
            existente.IdSubCategoria = request.IdSubCategoria;
            existente.CodigoBarra = request.CodigoBarra;
            existente.Nombre = request.Nombre;
            existente.Stock = request.Stock;
            existente.StockMinimo = request.StockMinimo;
            existente.PrecioVenta = request.PrecioVenta;
            existente.PrecioCompra = request.PrecioCompra;
            existente.IdUnidadMedida = request.IdUnidadMedida;
            existente.Estado = request.Estado;

            // FOTO: request.Foto viene ya con la ruta antigua o la nueva
            existente.Foto = request.Foto;

            existente.Mpn = request.Mpn;
            existente.ShippingInfo = request.ShippingInfo;
            existente.Material = request.Material;
            existente.Color = request.Color;
            existente.Genero = request.Genero;
            existente.Articulo = request.Articulo;
            existente.Estilo = request.Estilo;

            await _productoRepositorio.Actualizar(existente);

            var actuales = existente.TallaProductos.ToList();
            foreach (var tp in actuales)
                await _tpRepositorio.EliminarTallaProducto(tp.IdProducto, tp.Usa);

            foreach (var size in request.Sizes)
                await _tpRepositorio.CrearTallaProducto(new TallaProducto
                {
                    IdProducto = id,
                    Usa = size.Usa,
                    Eur = size.Eur,
                    Cm = size.Cm,
                    Stock = size.Stock
                });

            return true;
        }

        public async Task<bool> EliminarProducto(int id)
            => await _productoRepositorio.Eliminar(id);
    }
}
