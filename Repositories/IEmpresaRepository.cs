using CH_BACKEND.DBCalzadosHuancayo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> GetEmpresasAsync();
        Task<Empresa?> GetEmpresaByIdAsync(int id);
        Task<Empresa> AddEmpresaAsync(Empresa empresa);
        Task<Empresa?> UpdateEmpresaAsync(Empresa empresa);
        Task<bool> DeleteEmpresaAsync(int id);
    }
}
