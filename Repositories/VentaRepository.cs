using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class VentaRepositorio
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public VentaRepositorio(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<Venta>> ObtenerTodas()
        {
            return await _context.Ventas
                .Include(v => v.IdUsuarioNavigation)
                .Include(v => v.DetalleVenta)
                .ToListAsync();
        }

        public async Task<Venta?> ObtenerPorId(int id)
        {
            return await _context.Ventas
                .Include(v => v.IdUsuarioNavigation)
                .Include(v => v.DetalleVenta)
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public async Task<Venta?> Crear(Venta venta)
        {
            await _context.Ventas.AddAsync(venta);
            var result = await _context.SaveChangesAsync() > 0;
            return result ? venta : null;
        }

        public async Task<bool> Actualizar(Venta venta)
        {
            _context.Ventas.Update(venta);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var venta = await ObtenerPorId(id);
            if (venta == null) return false;

            _context.Ventas.Remove(venta);
            return await _context.SaveChangesAsync() > 0;
        }

        // Método agregado para guardar cambios de forma asíncrona.
        public async Task<bool> GuardarCambiosAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
