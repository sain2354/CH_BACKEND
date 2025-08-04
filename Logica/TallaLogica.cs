
using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Models;
using CH_BACKEND.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class TallaLogica
    {
        private readonly TallaRepository _repo;

        public TallaLogica(TallaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TallaResponse>> ObtenerTallas(string? categoria)
        {
            var list = await _repo.ObtenerTallas(categoria);
            return list.Select(t => new TallaResponse
            {
                IdTalla = t.IdTalla,
                Categoria = t.Categoria,
                Usa = t.Usa,
                Eur = t.Eur,
                Cm = t.Cm
            }).ToList();
        }

        public async Task<TallaResponse?> ObtenerTallaPorId(int id)
        {
            var t = await _repo.ObtenerTallaPorId(id);
            if (t == null) return null;
            return new TallaResponse
            {
                IdTalla = t.IdTalla,
                Categoria = t.Categoria,
                Usa = t.Usa,
                Eur = t.Eur,
                Cm = t.Cm
            };
        }

        public async Task<TallaResponse> CrearTalla(TallaRequest req)
        {
            var entity = new Talla
            {
                Categoria = req.Categoria,
                Usa = req.Usa,
                Eur = req.Eur,
                Cm = req.Cm
            };
            await _repo.CrearTalla(entity);
            return new TallaResponse
            {
                IdTalla = entity.IdTalla,
                Categoria = entity.Categoria,
                Usa = entity.Usa,
                Eur = entity.Eur,
                Cm = entity.Cm
            };
        }

        public async Task<bool> ActualizarTalla(int id, TallaRequest req)
        {
            var entity = await _repo.ObtenerTallaPorId(id);
            if (entity == null) return false;
            entity.Categoria = req.Categoria;
            entity.Usa = req.Usa;
            entity.Eur = req.Eur;
            entity.Cm = req.Cm;
            await _repo.ActualizarTalla(entity);
            return true;
        }
    }
}