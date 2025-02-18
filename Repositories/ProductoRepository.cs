using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class ProductoRepositorio
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public ProductoRepositorio(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ObtenerTodos()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<bool> Crear(Producto producto)
        {
            await _context.Productos.AddAsync(producto);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Actualizar(Producto producto)
        {
            _context.Productos.Update(producto);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var producto = await ObtenerPorId(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
