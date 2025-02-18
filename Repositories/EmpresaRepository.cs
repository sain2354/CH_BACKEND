using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public EmpresaRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetEmpresasAsync()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task<Empresa?> GetEmpresaByIdAsync(int id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task<Empresa> AddEmpresaAsync(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
            return empresa;
        }

        public async Task<Empresa?> UpdateEmpresaAsync(Empresa empresa)
        {
            var empresaExistente = await _context.Empresas.FindAsync(empresa.IdEmpresa);
            if (empresaExistente == null)
                return null;

            _context.Entry(empresaExistente).CurrentValues.SetValues(empresa);
            await _context.SaveChangesAsync();
            return empresaExistente;
        }

        public async Task<bool> DeleteEmpresaAsync(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
                return false;

            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
