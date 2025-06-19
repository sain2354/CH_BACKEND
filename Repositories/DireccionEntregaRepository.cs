using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class DireccionEntregaRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public DireccionEntregaRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        // Obtener todas
        public async Task<List<DireccionEntrega>> ObtenerTodas()
        {
            return await _context.DireccionEntregas.ToListAsync();
        }

        // Obtener por Id
        public async Task<DireccionEntrega?> ObtenerPorId(int id)
        {
            return await _context.DireccionEntregas
                .FirstOrDefaultAsync(d => d.IdDireccionEntrega == id);
        }

        // Crear
        public async Task<DireccionEntrega> Crear(DireccionEntrega entidad)
        {
            _context.DireccionEntregas.Add(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }

        // Actualizar
        public async Task<bool> Actualizar(DireccionEntrega entidad)
        {
            _context.DireccionEntregas.Update(entidad);
            return await _context.SaveChangesAsync() > 0;
        }

        // Eliminar
        public async Task<bool> Eliminar(int id)
        {
            var direccion = await ObtenerPorId(id);
            if (direccion == null) return false;

            _context.DireccionEntregas.Remove(direccion);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
