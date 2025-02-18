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

        public async Task<bool> Crear(Persona persona)
        {
            _context.Personas.Add(persona);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Actualizar(Persona persona)
        {
            _context.Personas.Update(persona);
            return await _context.SaveChangesAsync() > 0;
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
