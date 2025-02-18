using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class SubCategoriaRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public SubCategoriaRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<SubCategoria>> ObtenerSubCategorias()
        {
            return await _context.SubCategoria.ToListAsync();
        }

        public async Task<SubCategoria?> ObtenerSubCategoriaPorId(int id)
        {
            return await _context.SubCategoria.FindAsync(id);
        }

        public async Task AgregarSubCategoria(SubCategoria subCategoria)
        {
            _context.SubCategoria.Add(subCategoria);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarSubCategoria(SubCategoria subCategoria)
        {
            _context.SubCategoria.Update(subCategoria);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarSubCategoria(int id)
        {
            var subCategoria = await _context.SubCategoria.FindAsync(id);
            if (subCategoria != null)
            {
                _context.SubCategoria.Remove(subCategoria);
                await _context.SaveChangesAsync();
            }
        }
    }
}
