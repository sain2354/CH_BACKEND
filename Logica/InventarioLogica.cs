// InventarioLogica.cs
using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;

namespace CH_BACKEND.Logica
{
    public class InventarioLogica
    {
        private readonly InventarioRepository _inventarioRepository;

        public InventarioLogica(InventarioRepository inventarioRepository)
        {
            _inventarioRepository = inventarioRepository;
        }

        public List<Inventario> ObtenerInventario()
        {
            return _inventarioRepository.ObtenerTodos();
        }

        public Inventario? ObtenerInventarioPorId(int id)
        {
            return _inventarioRepository.ObtenerPorId(id);
        }

        public List<Inventario> FiltrarPorProductoYFecha(int idProducto, DateOnly fecha)
        {
            return _inventarioRepository.FiltrarPorProductoYFecha(idProducto, fecha);
        }

        public void CrearInventario(InventarioRequest request)
        {
            var inventario = new Inventario
            {
                IdProducto = request.IdProducto,
                CantidadInventario = request.CantidadInventario,
                FechaRegistro = request.FechaRegistro,
                TipoMovimiento = request.TipoMovimiento
            };

            _inventarioRepository.Agregar(inventario);
        }
    }
}