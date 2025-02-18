using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class UnidadMedidaLogica
    {
        private readonly UnidadMedidaRepository _unidadMedidaRepository;

        public UnidadMedidaLogica(UnidadMedidaRepository unidadMedidaRepository)
        {
            _unidadMedidaRepository = unidadMedidaRepository;
        }

        public async Task<List<UnidadMedida>> ObtenerUnidadesMedida()
        {
            return await _unidadMedidaRepository.ObtenerUnidadesMedida();
        }

        public async Task<UnidadMedida> ObtenerUnidadMedidaPorId(int id)
        {
            return await _unidadMedidaRepository.ObtenerUnidadMedidaPorId(id);
        }
    }
}
