using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Logica
{
    public class EmpresaLogica
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaLogica(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public async Task<IEnumerable<Empresa>> ObtenerEmpresasAsync()
        {
            return await _empresaRepository.GetEmpresasAsync();
        }

        public async Task<Empresa?> ObtenerEmpresaPorIdAsync(int id)
        {
            return await _empresaRepository.GetEmpresaByIdAsync(id);
        }

        public async Task<Empresa> CrearEmpresaAsync(Empresa empresa)
        {
            return await _empresaRepository.AddEmpresaAsync(empresa);
        }

        public async Task<Empresa?> ActualizarEmpresaAsync(Empresa empresa)
        {
            return await _empresaRepository.UpdateEmpresaAsync(empresa);
        }

        public async Task<bool> EliminarEmpresaAsync(int id)
        {
            return await _empresaRepository.DeleteEmpresaAsync(id);
        }
    }
}
