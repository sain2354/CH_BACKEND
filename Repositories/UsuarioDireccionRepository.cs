using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CH_BACKEND.Repositories
{
    public class UsuarioDireccionRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public UsuarioDireccionRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        // Obtener todas las direcciones
        public async Task<List<UsuarioDireccion>> ObtenerTodas()
        {
            return await _context.UsuarioDireccions.ToListAsync();
        }

        // Obtener direcciones por IdUsuario
        public async Task<List<UsuarioDireccion>> ObtenerPorUsuario(int idUsuario)
        {
            return await _context.UsuarioDireccions
                .Where(d => d.IdUsuario == idUsuario)
                .ToListAsync();
        }

        // Obtener una dirección por su Id
        public async Task<UsuarioDireccion?> ObtenerPorId(int idDireccion)
        {
            return await _context.UsuarioDireccions
                .FirstOrDefaultAsync(d => d.IdDireccion == idDireccion);
        }

        // Crear una nueva dirección
        public async Task<UsuarioDireccion> Crear(UsuarioDireccion entidad)
        {
            entidad.FechaRegistro = System.DateTime.Now;
            _context.UsuarioDireccions.Add(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }

        // Actualizar dirección existente
        public async Task<bool> Actualizar(UsuarioDireccion entidad)
        {
            _context.UsuarioDireccions.Update(entidad);
            return await _context.SaveChangesAsync() > 0;
        }

        // Eliminar dirección
        public async Task<bool> Eliminar(int idDireccion)
        {
            var direccion = await ObtenerPorId(idDireccion);
            if (direccion == null) return false;

            _context.UsuarioDireccions.Remove(direccion);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
