using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CH_BACKEND.DBCalzadosHuancayo;

namespace CH_BACKEND.Logica
{
    public class RolLogica
    {
        private readonly RolRepository _rolRepository;

        public RolLogica(RolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<List<Rol>> ObtenerRolesAsync()
        {
            return await _rolRepository.ObtenerRolesAsync();
        }

        public async Task<Rol?> ObtenerRolPorIdAsync(int id)
        {
            return await _rolRepository.ObtenerRolPorIdAsync(id);
        }

        public async Task<bool> AgregarRolAsync(string nombre, string? descripcion)
        {
            var rol = new Rol
            {
                Nombre = nombre,
                Descripcion = descripcion
            };

            await _rolRepository.AgregarRolAsync(rol);
            return true;
        }

        public async Task<bool> ActualizarRolAsync(int id, string nombre, string? descripcion)
        {
            var rol = new Rol
            {
                IdRol = id,
                Nombre = nombre,
                Descripcion = descripcion
            };

            return await _rolRepository.ActualizarRolAsync(rol);
        }

        public async Task<bool> EliminarRolAsync(int id)
        {
            return await _rolRepository.EliminarRolAsync(id);
        }
    }
}
