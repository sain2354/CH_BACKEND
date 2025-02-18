using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class TallaLogica
    {
        private readonly TallaRepository _tallaRepository;

        public TallaLogica(TallaRepository tallaRepository)
        {
            _tallaRepository = tallaRepository;
        }

        public async Task<List<TallaResponse>> ObtenerTallas()
        {
            var tallas = await _tallaRepository.ObtenerTallas();
            return tallas.Select(t => new TallaResponse
            {
                IdTalla = t.IdTalla,
                Descripcion = t.Descripcion
            }).ToList();
        }

        public async Task<TallaResponse?> ObtenerTallaPorId(int id)
        {
            var talla = await _tallaRepository.ObtenerTallaPorId(id);
            if (talla == null) return null;

            return new TallaResponse
            {
                IdTalla = talla.IdTalla,
                Descripcion = talla.Descripcion
            };
        }
    }
}
