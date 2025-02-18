using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;

namespace CH_BACKEND.Logica
{
    public class HistorialInventarioLogica
    {
        private readonly HistorialInventarioRepository _historialInventarioRepository;

        public HistorialInventarioLogica(HistorialInventarioRepository historialInventarioRepository)
        {
            _historialInventarioRepository = historialInventarioRepository;
        }

        public List<HistorialInventario> ObtenerHistorial() // Renombrado
        {
            return _historialInventarioRepository.ObtenerTodos();
        }

        public HistorialInventario? ObtenerHistorialPorId(int id)
        {
            return _historialInventarioRepository.ObtenerPorId(id);
        }

        public void CrearHistorial(HistorialInventarioRequest request)
        {
            var historial = new HistorialInventario
            {
                IdProducto = request.IdProducto,
                TipoMovimiento = request.TipoMovimiento,
                Cantidad = request.Cantidad,
                Fecha = request.Fecha,
                UsuarioResponsable = request.UsuarioResponsable,
                DocumentoCompra = request.DocumentoCompra,
                IdProveedor = request.IdProveedor,
                ProveedorNombre = request.ProveedorNombre,
                ProveedorContacto = request.ProveedorContacto,
                CompraTotal = request.CompraTotal,
                CompraFecha = request.CompraFecha
            };

            _historialInventarioRepository.Agregar(historial);
        }
    }
}
