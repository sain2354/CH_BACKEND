// Repositories/ProductoRepositorio.cs
using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Producto>> ObtenerPorFiltros(
            int cat,
            string? genero,
            string? articulo,
            string? estilo)
        {
            var query = _context.Productos
                .Include(p => p.IdSubCategoriaNavigation)
                .Include(p => p.TallaProductos)
                .AsQueryable();

            if (cat != 0)
                query = query.Where(p => p.IdCategoria == cat);

            if (!string.IsNullOrEmpty(genero))
                query = query.Where(p => p.Genero == genero);

            if (!string.IsNullOrEmpty(articulo))
                query = query.Where(p => p.Articulo == articulo);


            if (!string.IsNullOrEmpty(estilo))
                query = query.Where(p => p.Estilo == estilo);

            return await query.ToListAsync();
        }

        public async Task<Producto?> ObtenerPorId(int id)
        {
            return await _context.Productos
                .Include(p => p.IdSubCategoriaNavigation)
                .Include(p => p.TallaProductos)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
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
            if (producto == null) return false;
            _context.Productos.Remove(producto);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
