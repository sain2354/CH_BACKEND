using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class RolRepository
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public RolRepository(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<Rol>> ObtenerRolesAsync()
        {
            // Se asume que en _DbContextCalzadosHuancayo tienes declarado un DbSet<Rol> llamado Rols
            return await _context.Rols.ToListAsync();
        }

        public async Task<Rol?> ObtenerRolPorIdAsync(int id)
        {
            return await _context.Rols.FindAsync(id);
        }

        public async Task AgregarRolAsync(Rol rol)
        {
            _context.Rols.Add(rol);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarRolAsync(int id)
        {
            var rol = await ObtenerRolPorIdAsync(id);
            if (rol == null) return false;

            _context.Rols.Remove(rol);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActualizarRolAsync(Rol rol)
        {
            var rolExistente = await ObtenerRolPorIdAsync(rol.IdRol);
            if (rolExistente == null) return false;

            rolExistente.Nombre = rol.Nombre;
            rolExistente.Descripcion = rol.Descripcion;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
