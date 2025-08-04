
using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Talla>> ObtenerTallas(string? categoria)
        {
            var query = _context.Tallas.AsQueryable();
            if (!string.IsNullOrEmpty(categoria))
            {
                query = query.Where(t => t.Categoria == categoria);
            }
            return await query.ToListAsync();
        }

        public async Task<Talla?> ObtenerTallaPorId(int id)
            => await _context.Tallas.FindAsync(id);

        public async Task CrearTalla(Talla talla)
        {
            _context.Tallas.Add(talla);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarTalla(Talla talla)
        {
            _context.Tallas.Update(talla);
            await _context.SaveChangesAsync();
        }
    }
}
