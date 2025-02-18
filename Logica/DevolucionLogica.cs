using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;

namespace CH_BACKEND.Logica
{
    public class DevolucionLogica
    {
        private readonly DevolucionRepository _devolucionRepository;

        public DevolucionLogica(DevolucionRepository devolucionRepository)
        {
            _devolucionRepository = devolucionRepository;
        }

        public List<DevolucionResponse> ObtenerDevoluciones()
        {
            return _devolucionRepository.ObtenerTodas()
                .Select(d => new DevolucionResponse(d))
                .ToList();
        }

        public DevolucionResponse? ObtenerDevolucionPorId(int id)
        {
            var devolucion = _devolucionRepository.ObtenerPorId(id);
            return devolucion == null ? null : new DevolucionResponse(devolucion);
        }

        public void CrearDevolucion(DevolucionRequest request)
        {
            var devolucion = new Devolucion
            {
                IdVenta = request.IdVenta,
                IdProducto = request.IdProducto,
                CantidadDevolucion = request.CantidadDevolucion,
                Fecha = request.Fecha,
                Motivo = request.Motivo
            };

            _devolucionRepository.Agregar(devolucion);
        }
    }
}
