using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class TallaRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public TallaRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<Talla>> ObtenerTallas()
        {
            return await _context.Tallas.ToListAsync();
        }

        public async Task<Talla?> ObtenerTallaPorId(int id)
        {
            return await _context.Tallas.FindAsync(id);
        }
    }
}
