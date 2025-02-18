using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Request;
using CH_BACKEND.Response;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CH_BACKEND.Logica
{
    public class PersonaLogica
    {
        private readonly PersonaRepositorio _personaRepositorio;

        public PersonaLogica(PersonaRepositorio personaRepositorio)
        {
            _personaRepositorio = personaRepositorio;
        }

        public async Task<List<PersonaResponse>> ObtenerTodos()
        {
            var personas = await _personaRepositorio.ObtenerTodos();
            return personas.Select(p => new PersonaResponse
            {
                IdPersona = p.IdPersona,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Correo = p.Correo,
                Direccion = p.Direccion,
                FechaRegistro = p.FechaRegistro,
                TipoPersona = p.TipoPersona,
                TipoDocumento = p.TipoDocumento,
                NumeroDocumento = p.NumeroDocumento
            }).ToList();
        }

        public async Task<PersonaResponse?> ObtenerPorId(int id)
        {
            var persona = await _personaRepositorio.ObtenerPorId(id);
            if (persona == null) return null;

            return new PersonaResponse
            {
                IdPersona = persona.IdPersona,
                Nombre = persona.Nombre,
                Telefono = persona.Telefono,
                Correo = persona.Correo,
                Direccion = persona.Direccion,
                FechaRegistro = persona.FechaRegistro,
                TipoPersona = persona.TipoPersona,
                TipoDocumento = persona.TipoDocumento,
                NumeroDocumento = persona.NumeroDocumento
            };
        }

        public async Task<bool> Crear(PersonaRequest request)
        {
            var persona = new Persona
            {
                Nombre = request.Nombre,
                Telefono = request.Telefono,
                Correo = request.Correo,
                Direccion = request.Direccion,
                FechaRegistro = request.FechaRegistro,
                TipoPersona = request.TipoPersona,
                TipoDocumento = request.TipoDocumento,
                NumeroDocumento = request.NumeroDocumento
            };

            return await _personaRepositorio.Crear(persona);
        }

        public async Task<bool> Actualizar(int id, PersonaRequest request)
        {
            var persona = await _personaRepositorio.ObtenerPorId(id);
            if (persona == null) return false;

            persona.Nombre = request.Nombre;
            persona.Telefono = request.Telefono;
            persona.Correo = request.Correo;
            persona.Direccion = request.Direccion;
            persona.FechaRegistro = request.FechaRegistro;
            persona.TipoPersona = request.TipoPersona;
            persona.TipoDocumento = request.TipoDocumento;
            persona.NumeroDocumento = request.NumeroDocumento;

            return await _personaRepositorio.Actualizar(persona);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _personaRepositorio.Eliminar(id);
        }
    }
}
