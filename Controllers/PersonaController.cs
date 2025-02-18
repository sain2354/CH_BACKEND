using CH_BACKEND.Logica;
using CH_BACKEND.Request;
using CH_BACKEND.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaLogica _personaLogica;

        public PersonaController(PersonaLogica personaLogica)
        {
            _personaLogica = personaLogica;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonaResponse>>> ObtenerTodos()
        {
            return Ok(await _personaLogica.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaResponse>> ObtenerPorId(int id)
        {
            var persona = await _personaLogica.ObtenerPorId(id);
            if (persona == null) return NotFound("Persona no encontrada.");
            return Ok(persona);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] PersonaRequest request)
        {
            var creado = await _personaLogica.Crear(request);
            if (!creado) return BadRequest("No se pudo crear la persona.");
            return Ok("Persona creada correctamente.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] PersonaRequest request)
        {
            var actualizado = await _personaLogica.Actualizar(id, request);
            if (!actualizado) return BadRequest("No se pudo actualizar la persona.");
            return Ok("Persona actualizada correctamente.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminado = await _personaLogica.Eliminar(id);
            if (!eliminado) return NotFound("No se encontró la persona.");
            return Ok("Persona eliminada correctamente.");
        }
    }
}
