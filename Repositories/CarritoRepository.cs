using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class CarritoRepositorio
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public CarritoRepositorio(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<Carrito>> ObtenerTodos()
        {
            return await _context.Carritos
                .Include(c => c.CarritoDetalles)
                .ToListAsync();
        }

        public async Task<Carrito> ObtenerPorId(int idCarrito)
        {
            return await _context.Carritos
                .Include(c => c.CarritoDetalles)
                .FirstOrDefaultAsync(c => c.IdCarrito == idCarrito);
        }

        public async Task<bool> Crear(Carrito carrito)
        {
            await _context.Carritos.AddAsync(carrito);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Actualizar(Carrito carrito)
        {
            _context.Carritos.Update(carrito);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int idCarrito)
        {
            var carrito = await ObtenerPorId(idCarrito);
            if (carrito == null) return false;

            _context.Carritos.Remove(carrito);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
