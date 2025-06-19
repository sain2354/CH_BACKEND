using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionEntregaController : ControllerBase
    {
        private readonly DireccionEntregaLogica _logica;

        public DireccionEntregaController(DireccionEntregaLogica logica)
        {
            _logica = logica;
        }

        // GET: api/DireccionEntrega
        [HttpGet]
        public async Task<ActionResult<List<DireccionEntregaResponse>>> GetAll()
        {
            var lista = await _logica.ObtenerTodas();
            return Ok(lista);
        }

        // GET: api/DireccionEntrega/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DireccionEntregaResponse>> GetOne(int id)
        {
            var direccion = await _logica.ObtenerPorId(id);
            if (direccion == null) return NotFound();
            return Ok(direccion);
        }

        // POST: api/DireccionEntrega
        [HttpPost]
        public async Task<ActionResult<DireccionEntregaResponse>> Create([FromBody] DireccionEntregaRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var creada = await _logica.Crear(request);
            return CreatedAtAction(nameof(GetOne), new { id = creada.IdDireccionEntrega }, creada);
        }

        // PUT: api/DireccionEntrega/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DireccionEntregaRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var resultado = await _logica.Actualizar(id, request);
            if (!resultado) return NotFound();
            return Ok("Dirección de entrega actualizada correctamente");
        }

        // DELETE: api/DireccionEntrega/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _logica.Eliminar(id);
            if (!eliminado) return NotFound();
            return Ok("Dirección de entrega eliminada correctamente");
        }
    }
}
