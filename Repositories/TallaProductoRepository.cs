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

        public TallaProductoRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<TallaProducto>> ObtenerTallaProductos()
        {
            return await _context.TallaProductos.ToListAsync();
        }

        // NUEVO: Obtener tallas de un producto
        public async Task<List<TallaProducto>> ObtenerTallasPorProducto(int idProducto)
        {
            // Filtra TallaProductos por idProducto
            return await _context.TallaProductos
                .Where(tp => tp.IdProducto == idProducto)
                .ToListAsync();
        }

        public async Task<TallaProducto?> ObtenerTallaProductoPorId(int idProducto, int idTalla)
        {
            return await _context.TallaProductos.FindAsync(idProducto, idTalla);
        }

        public async Task CrearTallaProducto(TallaProducto tallaProducto)
        {
            _context.TallaProductos.Add(tallaProducto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarTallaProducto(TallaProducto tallaProducto)
        {
            _context.TallaProductos.Update(tallaProducto);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarTallaProducto(int idProducto, int idTalla)
        {
            var tallaProducto = await _context.TallaProductos.FindAsync(idProducto, idTalla);
            if (tallaProducto == null) return false;

            _context.TallaProductos.Remove(tallaProducto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
