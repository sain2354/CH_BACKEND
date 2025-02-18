using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;  // Asegúrate de que el modelo Promocion esté en este namespace
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class PromocionLogica
    {
        private readonly PromocionRepository _promocionRepository;

        public PromocionLogica(PromocionRepository promocionRepository)
        {
            _promocionRepository = promocionRepository;
        }

        public Task<List<Promocion>> ObtenerPromociones() =>
            _promocionRepository.ObtenerPromociones();

        public Task<Promocion?> ObtenerPromocionPorId(int id) =>
            _promocionRepository.ObtenerPromocionPorId(id);

        public async Task AgregarPromocion(Promocion promocion)
        {
            if (promocion == null)
                throw new ArgumentNullException(nameof(promocion));
            await _promocionRepository.AgregarPromocion(promocion);
        }

        public async Task ActualizarPromocion(Promocion promocion)
        {
            if (promocion == null)
                throw new ArgumentNullException(nameof(promocion));
            await _promocionRepository.ActualizarPromocion(promocion);
        }

        public Task EliminarPromocion(int id) =>
            _promocionRepository.EliminarPromocion(id);
    }
}
