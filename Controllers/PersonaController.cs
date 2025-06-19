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
        public async Task<ActionResult> ObtenerTodos()
        {
            var lista = await _personaLogica.ObtenerTodos();
            return Ok(new { success = true, data = lista });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ObtenerPorId(int id)
        {
            var persona = await _personaLogica.ObtenerPorId(id);
            if (persona == null)
                return NotFound(new { success = false, message = "Persona no encontrada." });

            return Ok(new { success = true, data = persona });
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] PersonaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Datos inválidos." });

            var personaCreada = await _personaLogica.Crear(request);
            return Ok(new { success = true, data = personaCreada });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] PersonaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, message = "Datos inválidos." });

            var personaActualizada = await _personaLogica.Actualizar(id, request);
            if (personaActualizada == null)
                return NotFound(new { success = false, message = "Persona no encontrada." });

            return Ok(new { success = true, data = personaActualizada });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminado = await _personaLogica.Eliminar(id);
            if (!eliminado)
                return NotFound(new { success = false, message = "No se encontró la persona." });

            return Ok(new { success = true, message = "Persona eliminada correctamente." });
        }
    }
}
