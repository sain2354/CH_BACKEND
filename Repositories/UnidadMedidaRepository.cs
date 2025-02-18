using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class UnidadMedidaRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public UnidadMedidaRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<UnidadMedida>> ObtenerUnidadesMedida()
        {
            return await _context.UnidadMedida.ToListAsync();
        }

        public async Task<UnidadMedida> ObtenerUnidadMedidaPorId(int id)
        {
            return await _context.UnidadMedida.FindAsync(id);
        }
    }
}