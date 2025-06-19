using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class DetalleVentaRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public DetalleVentaRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<DetalleVenta>> ObtenerTodos()
        {
            return await _context.DetalleVenta
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdVentaNavigation)
                .Include(d => d.IdUnidadMedidaNavigation)
                .ToListAsync();
        }

        public async Task<DetalleVenta?> ObtenerPorId(int id)
        {
            return await _context.DetalleVenta
                .Include(d => d.IdProductoNavigation)
                .Include(d => d.IdVentaNavigation)
                .Include(d => d.IdUnidadMedidaNavigation)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<bool> Crear(DetalleVenta detalleVenta)
        {
            _context.DetalleVenta.Add(detalleVenta);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Actualizar(DetalleVenta detalleVenta)
        {
            _context.DetalleVenta.Update(detalleVenta);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var detalleVenta = await _context.DetalleVenta.FindAsync(id);
            if (detalleVenta == null) return false;

            _context.DetalleVenta.Remove(detalleVenta);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
