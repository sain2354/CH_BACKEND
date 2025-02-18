using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class TallaProductoLogica
    {
        private readonly TallaProductoRepository _tallaProductoRepository;

        public TallaProductoLogica(TallaProductoRepository tallaProductoRepository)
        {
            _tallaProductoRepository = tallaProductoRepository;
        }

        public async Task<List<TallaProductoResponse>> ObtenerTallaProductos()
        {
            var lista = await _tallaProductoRepository.ObtenerTallaProductos();
            return lista.Select(tp => new TallaProductoResponse
            {
                IdProducto = tp.IdProducto,
                IdTalla = tp.IdTalla,
                Stock = tp.Stock
            }).ToList();
        }

        public async Task<TallaProductoResponse?> ObtenerTallaProductoPorId(int idProducto, int idTalla)
        {
            var tallaProducto = await _tallaProductoRepository.ObtenerTallaProductoPorId(idProducto, idTalla);
            if (tallaProducto == null) return null;

            return new TallaProductoResponse
            {
                IdProducto = tallaProducto.IdProducto,
                IdTalla = tallaProducto.IdTalla,
                Stock = tallaProducto.Stock
            };
        }

        public async Task<TallaProductoResponse> CrearTallaProducto(TallaProductoRequest request)
        {
            var nuevo = new TallaProducto
            {
                IdProducto = request.IdProducto,
                IdTalla = request.IdTalla,
                Stock = request.Stock
            };

            await _tallaProductoRepository.CrearTallaProducto(nuevo);

            return new TallaProductoResponse
            {
                IdProducto = nuevo.IdProducto,
                IdTalla = nuevo.IdTalla,
                Stock = nuevo.Stock
            };
        }

        public async Task<bool> ActualizarTallaProducto(int idProducto, int idTalla, TallaProductoRequest request)
        {
            var tallaProducto = await _tallaProductoRepository.ObtenerTallaProductoPorId(idProducto, idTalla);
            if (tallaProducto == null) return false;

            tallaProducto.Stock = request.Stock;
            await _tallaProductoRepository.ActualizarTallaProducto(tallaProducto);

            return true;
        }

        public async Task<bool> EliminarTallaProducto(int idProducto, int idTalla)
        {
            return await _tallaProductoRepository.EliminarTallaProducto(idProducto, idTalla);
        }
    }
}
