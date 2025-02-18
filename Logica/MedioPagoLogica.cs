using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using System.Collections.Generic;

namespace CH_BACKEND.Logica
{
    public class MedioPagoLogica
    {
        private readonly MedioPagoRepository _medioPagoRepository;

        public MedioPagoLogica(MedioPagoRepository medioPagoRepository)
        {
            _medioPagoRepository = medioPagoRepository;
        }

        public List<MedioPago> ObtenerMediosPago()
        {
            return _medioPagoRepository.ObtenerTodos();
        }

        public MedioPago? ObtenerMedioPagoPorId(int id)
        {
            return _medioPagoRepository.ObtenerPorId(id);
        }

        public void CrearMedioPago(MedioPago medioPago)
        {
            _medioPagoRepository.Agregar(medioPago);
        }

        public void ActualizarMedioPago(MedioPago medioPago)
        {
            _medioPagoRepository.Actualizar(medioPago);
        }

        public void EliminarMedioPago(int id)
        {
            _medioPagoRepository.Eliminar(id);
        }
    }
}
