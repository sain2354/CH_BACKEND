using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class ProductoLogica
    {
        private readonly ProductoRepositorio _productoRepositorio;

        public ProductoLogica(ProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        // Filtra sólo por categoría, sin paginar
        public async Task<List<ProductoResponse>> ObtenerProductosPorCategoria(int cat)
        {
            var lista = await _productoRepositorio.ObtenerPorCategoria(cat);
            // Convertimos a ProductoResponse
            var productosResp = lista.ConvertAll(p => new ProductoResponse(p));
            return productosResp;
        }

        public async Task<ProductoResponse> ObtenerProductoPorId(int id)
        {
            var producto = await _productoRepositorio.ObtenerPorId(id);
            return producto != null ? new ProductoResponse(producto) : null;
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
                Foto = request.Foto
            };

            var resultado = await _productoRepositorio.Crear(producto);
            return resultado ? producto.IdProducto : 0;
        }

        public async Task<bool> ActualizarProducto(int id, ProductoRequest request)
        {
            var productoExistente = await _productoRepositorio.ObtenerPorId(id);
            if (productoExistente == null) return false;

            productoExistente.IdCategoria = request.IdCategoria;
            productoExistente.IdSubCategoria = request.IdSubCategoria;
            productoExistente.CodigoBarra = request.CodigoBarra;
            productoExistente.Nombre = request.Nombre;
            productoExistente.Stock = request.Stock;
            productoExistente.StockMinimo = request.StockMinimo;
            productoExistente.PrecioVenta = request.PrecioVenta;
            productoExistente.PrecioCompra = request.PrecioCompra;
            productoExistente.IdUnidadMedida = request.IdUnidadMedida;
            productoExistente.Estado = request.Estado;
            productoExistente.Foto = request.Foto;

            return await _productoRepositorio.Actualizar(productoExistente);
        }

        public async Task<bool> EliminarProducto(int id)
        {
            return await _productoRepositorio.Eliminar(id);
        }
    }
}
