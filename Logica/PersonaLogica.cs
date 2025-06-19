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

        // Ahora devolvemos PersonaResponse en lugar de bool
        public async Task<PersonaResponse> Crear(PersonaRequest request)
        {
            var personaEntidad = new Persona
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

            var creado = await _personaRepositorio.Crear(personaEntidad);

            return new PersonaResponse
            {
                IdPersona = creado.IdPersona,
                Nombre = creado.Nombre,
                Telefono = creado.Telefono,
                Correo = creado.Correo,
                Direccion = creado.Direccion,
                FechaRegistro = creado.FechaRegistro,
                TipoPersona = creado.TipoPersona,
                TipoDocumento = creado.TipoDocumento,
                NumeroDocumento = creado.NumeroDocumento
            };
        }

        // Devolvemos PersonaResponse tras actualizar
        public async Task<PersonaResponse?> Actualizar(int id, PersonaRequest request)
        {
            var personaEntidad = await _personaRepositorio.ObtenerPorId(id);
            if (personaEntidad == null) return null;

            personaEntidad.Nombre = request.Nombre;
            personaEntidad.Telefono = request.Telefono;
            personaEntidad.Correo = request.Correo;
            personaEntidad.Direccion = request.Direccion;
            personaEntidad.FechaRegistro = request.FechaRegistro;
            personaEntidad.TipoPersona = request.TipoPersona;
            personaEntidad.TipoDocumento = request.TipoDocumento;
            personaEntidad.NumeroDocumento = request.NumeroDocumento;

            var actualizado = await _personaRepositorio.Actualizar(personaEntidad);

            return new PersonaResponse
            {
                IdPersona = actualizado.IdPersona,
                Nombre = actualizado.Nombre,
                Telefono = actualizado.Telefono,
                Correo = actualizado.Correo,
                Direccion = actualizado.Direccion,
                FechaRegistro = actualizado.FechaRegistro,
                TipoPersona = actualizado.TipoPersona,
                TipoDocumento = actualizado.TipoDocumento,
                NumeroDocumento = actualizado.NumeroDocumento
            };
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _personaRepositorio.Eliminar(id);
        }
    }
}
