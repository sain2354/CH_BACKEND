using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Repositories
{
    public class PersonaRepositorio
    {
        private readonly _DbContextCalzadosHuancayo _context;

        public PersonaRepositorio(_DbContextCalzadosHuancayo context)
        {
            _context = context;
        }

        public async Task<List<Persona>> ObtenerTodos()
        {
            return await _context.Personas.ToListAsync();
        }

        public async Task<Persona?> ObtenerPorId(int id)
        {
            return await _context.Personas.FindAsync(id);
        }

        // Cambiamos el método Crear para que devuelva la entidad recién guardada (con Id)
        public async Task<Persona> Crear(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            return persona;
        }

        // Cambiamos el método Actualizar para que devuelva la entidad actualizada
        public async Task<Persona> Actualizar(Persona persona)
        {
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();
            return persona;
        }

        public async Task<bool> Eliminar(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null) return false;

            _context.Personas.Remove(persona);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
