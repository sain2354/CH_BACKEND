using CH_BACKEND.Models;
using CH_BACKEND.Logica;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioDireccionController : ControllerBase
    {
        private readonly UsuarioDireccionLogica _logica;

        public UsuarioDireccionController(UsuarioDireccionLogica logica)
        {
            _logica = logica;
        }

        // GET: api/UsuarioDireccion
        [HttpGet]
        public async Task<ActionResult<List<UsuarioDireccionResponse>>> GetAll()
        {
            var lista = await _logica.ObtenerTodas();
            return Ok(lista);
        }

        // GET: api/UsuarioDireccion/usuario/5
        [HttpGet("usuario/{idUsuario}")]
        public async Task<ActionResult<List<UsuarioDireccionResponse>>> GetByUser(int idUsuario)
        {
            var lista = await _logica.ObtenerPorUsuario(idUsuario);
            return Ok(lista);
        }

        // GET: api/UsuarioDireccion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDireccionResponse>> GetOne(int id)
        {
            var direccion = await _logica.ObtenerPorId(id);
            if (direccion == null) return NotFound();
            return Ok(direccion);
        }

        // POST: api/UsuarioDireccion
        [HttpPost]
        public async Task<ActionResult<UsuarioDireccionResponse>> Create([FromBody] UsuarioDireccionRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var creada = await _logica.Crear(request);
            return CreatedAtAction(nameof(GetOne), new { id = creada.IdDireccion }, creada);
        }

        // PUT: api/UsuarioDireccion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDireccionRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _logica.Actualizar(id, request);
            if (!resultado) return NotFound();
            return Ok("Dirección actualizada correctamente");
        }

        // DELETE: api/UsuarioDireccion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _logica.Eliminar(id);
            if (!eliminado) return NotFound();
            return Ok("Dirección eliminada correctamente");
        }
    }
}
