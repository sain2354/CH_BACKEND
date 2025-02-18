using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System;
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
            return await _context.Venta
                .Include(v => v.IdPersonaNavigation)
                .Include(v => v.DetalleVenta)
                .ToListAsync();
        }

        public async Task<Venta?> ObtenerPorId(int id)
        {
            return await _context.Venta
                .Include(v => v.IdPersonaNavigation)
                .Include(v => v.DetalleVenta)
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public async Task<bool> Crear(Venta venta)
        {
            await _context.Venta.AddAsync(venta);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Actualizar(Venta venta)
        {
            _context.Venta.Update(venta);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var venta = await ObtenerPorId(id);
            if (venta == null) return false;

            _context.Venta.Remove(venta);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
