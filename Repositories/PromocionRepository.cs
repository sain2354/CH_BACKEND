using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using CH_BACKEND.Models; // Asegúrate de tener el modelo Promocion

namespace CH_BACKEND.Repositories
{
    public class PromocionRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public PromocionRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<Promocion>> ObtenerPromociones()
        {
            return await _context.Promocions.AsNoTracking().ToListAsync();
        }

        public async Task<Promocion?> ObtenerPromocionPorId(int id)
        {
            return await _context.Promocions.AsNoTracking().FirstOrDefaultAsync(p => p.IdPromocion == id);
        }

        public async Task AgregarPromocion(Promocion promocion)
        {
            _context.Promocions.Add(promocion);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarPromocion(Promocion promocion)
        {
            _context.Entry(promocion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarPromocion(int id)
        {
            var promocion = await _context.Promocions.FindAsync(id);
            if (promocion != null)
            {
                _context.Promocions.Remove(promocion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
