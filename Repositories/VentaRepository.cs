using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
                .Include(v => v.Pagos)
                .ToListAsync();
        }

        public async Task<Venta?> ObtenerPorId(int id)
        {
            return await _context.Ventas
                .Include(v => v.IdUsuarioNavigation)
                .Include(v => v.DetalleVenta)
                .Include(v => v.Pagos)
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

        public async Task<Venta?> Crear(Venta venta)
        {
            await _context.Ventas.AddAsync(venta);
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? venta : null;
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

        public async Task<bool> GuardarCambiosAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task AgregarPagoAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
        }

        public async Task AgregarHistorialAsync(VentaEstadoHistorial hist)
        {
            _context.VentaEstadoHistorials.Add(hist);
            await _context.SaveChangesAsync();
        }

        public async Task<Venta?> ObtenerConDetalle(int id)
        {
            return await _context.Ventas
                .Include(v => v.IdUsuarioNavigation)
                    .ThenInclude(u => u.UsuarioDireccions)
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdProductoNavigation)          // PARA EL NOMBRE E IMAGEN
                .Include(v => v.DetalleVenta)
                    .ThenInclude(d => d.IdUnidadMedidaNavigation)      // PARA LA TALLA
                .Include(v => v.Pagos)
                .FirstOrDefaultAsync(v => v.IdVenta == id);
        }

    }
}
