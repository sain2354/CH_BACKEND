using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class TallaProductoRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public TallaProductoRepository(_DbContextCalzadosHuancayo ctx)
        {
            _context = ctx;
        }

        public async Task<List<TallaProducto>> ObtenerTallasPorProducto(int idProducto)
        {
            return await _context.TallaProductos
                .Where(tp => tp.IdProducto == idProducto)
                .ToListAsync();
        }

        public async Task<TallaProducto?> ObtenerTallaProductoPorId(int idProducto, int usa)
        {
            return await _context.TallaProductos
                .FindAsync(idProducto, usa);
        }

        public async Task CrearTallaProducto(TallaProducto entity)
        {
            _context.TallaProductos.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarTallaProducto(TallaProducto entity)
        {
            _context.TallaProductos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarTallaProducto(int idProducto, int usa)
        {
            var entity = await _context.TallaProductos.FindAsync(idProducto, usa);
            if (entity == null) return false;
            _context.TallaProductos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
