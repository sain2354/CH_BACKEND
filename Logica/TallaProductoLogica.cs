using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Models;
using CH_BACKEND.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class TallaProductoLogica
    {
        private readonly TallaProductoRepository _repo;

        public TallaProductoLogica(TallaProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TallaProductoResponse>> ObtenerTallasPorProducto(int idProducto)
        {
            var list = await _repo.ObtenerTallasPorProducto(idProducto);
            return list.Select(tp => new TallaProductoResponse
            {
                IdProducto = tp.IdProducto,
                Usa = tp.Usa,
                Eur = tp.Eur,
                Cm = tp.Cm,
                Stock = tp.Stock
            }).ToList();
        }

        public async Task<TallaProductoResponse> CrearTallaProducto(TallaProductoRequest req)
        {
            var entity = new TallaProducto
            {
                IdProducto = req.IdProducto,
                Usa = req.Usa,
                Eur = req.Eur,
                Cm = req.Cm,
                Stock = req.Stock
            };
            await _repo.CrearTallaProducto(entity);
            return new TallaProductoResponse
            {
                IdProducto = entity.IdProducto,
                Usa = entity.Usa,
                Eur = entity.Eur,
                Cm = entity.Cm,
                Stock = entity.Stock
            };
        }

        public async Task<bool> ActualizarTallaProducto(int idProducto, int usa, TallaProductoRequest req)
        {
            var entity = await _repo.ObtenerTallaProductoPorId(idProducto, usa);
            if (entity == null) return false;
            entity.Eur = req.Eur;
            entity.Cm = req.Cm;
            entity.Stock = req.Stock;
            await _repo.ActualizarTallaProducto(entity);
            return true;
        }

        public async Task<bool> EliminarTallaProducto(int idProducto, int usa)
        {
            return await _repo.EliminarTallaProducto(idProducto, usa);
        }
    }
}
