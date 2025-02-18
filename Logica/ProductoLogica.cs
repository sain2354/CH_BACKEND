using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CH_BACKEND.DBCalzadosHuancayo;

namespace CH_BACKEND.Logica
{
    public class ProductoLogica
    {
        private readonly ProductoRepositorio _productoRepositorio;

        public ProductoLogica(ProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        public async Task<List<ProductoResponse>> ObtenerProductos()
        {
            var productos = await _productoRepositorio.ObtenerTodos();
            return productos.ConvertAll(p => new ProductoResponse(p));
        }

        public async Task<ProductoResponse> ObtenerProductoPorId(int id)
        {
            var producto = await _productoRepositorio.ObtenerPorId(id);
            return producto != null ? new ProductoResponse(producto) : null;
        }

        public async Task<bool> AgregarProducto(ProductoRequest request)
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

            return await _productoRepositorio.Crear(producto);
        }

        public async Task<bool> ActualizarProducto(int id, ProductoRequest request)
        {
            var productoExistente = await _productoRepositorio.ObtenerPorId(id);
            if (productoExistente == null) return false;

            productoExistente.Nombre = request.Nombre;
            productoExistente.Stock = request.Stock;
            productoExistente.PrecioVenta = request.PrecioVenta;
            productoExistente.PrecioCompra = request.PrecioCompra;
            productoExistente.Estado = request.Estado;
            productoExistente.Foto = request.Foto;

            return await _productoRepositorio.Actualizar(productoExistente);
        }

        public async Task<bool> EliminarProducto(int id)
        {
            var producto = await _productoRepositorio.ObtenerPorId(id);
            if (producto == null) return false;

            return await _productoRepositorio.Eliminar(id);
        }
    }
}
