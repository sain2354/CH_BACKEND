using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class CategoriaRepositorio
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public CategoriaRepositorio(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> ObtenerTodas()
        {
            return await _context.Categoria.ToListAsync();
        }

        public async Task<Categoria?> ObtenerPorId(int id)
        {
            return await _context.Categoria.FindAsync(id);
        }

        public async Task<bool> Crear(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Actualizar(Categoria categoria)
        {
            _context.Categoria.Update(categoria);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null) return false;

            _context.Categoria.Remove(categoria);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
