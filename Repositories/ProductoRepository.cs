using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Filtra sólo por categoría, sin paginar
        public async Task<List<Producto>> ObtenerPorCategoria(int cat)
        {
            var query = _context.Productos
                .Include(p => p.IdSubCategoriaNavigation)
                .Include(p => p.TallaProductos)
                    .ThenInclude(tp => tp.IdTallaNavigation)
                .AsQueryable();

            if (cat != 0)
            {
                query = query.Where(p => p.IdCategoria == cat);
            }

            return await query.ToListAsync();
        }

        public async Task<Producto> ObtenerPorId(int id)
        {
            return await _context.Productos
                .Include(p => p.IdSubCategoriaNavigation)
                .Include(p => p.TallaProductos)
                    .ThenInclude(tp => tp.IdTallaNavigation)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<bool> Crear(Producto producto)
        {
            try
            {
                await _context.Productos.AddAsync(producto);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar en la base de datos: {ex}");
                throw;
            }
        }

        public async Task<bool> Actualizar(Producto producto)
        {
            _context.Productos.Update(producto);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var producto = await ObtenerPorId(id);
            if (producto == null) return false;

            _context.Productos.Remove(producto);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
