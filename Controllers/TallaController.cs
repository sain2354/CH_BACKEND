using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TallaController : ControllerBase
    {
        private readonly TallaLogica _logic;

        public TallaController(TallaLogica logic)
        {
            _logic = logic;
        }

        // GET /api/Talla?categoria=Hombres
        [HttpGet]
        public async Task<ActionResult<List<TallaResponse>>> ObtenerTallas([FromQuery] string? categoria = null)
        {
            var lista = await _logic.ObtenerTallas(categoria);
            return Ok(lista);
        }

        // GET /api/Talla/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TallaResponse>> ObtenerTallaPorId(int id)
        {
            var t = await _logic.ObtenerTallaPorId(id);
            if (t == null) return NotFound(new { mensaje = "Talla no encontrada" });
            return Ok(t);
        }

        // POST /api/Talla
        [HttpPost]
        public async Task<ActionResult<TallaResponse>> CrearTalla([FromBody] TallaRequest req)
        {
            var created = await _logic.CrearTalla(req);
            return CreatedAtAction(nameof(ObtenerTallaPorId), new { id = created.IdTalla }, created);
        }

        // PUT /api/Talla/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTalla(int id, [FromBody] TallaRequest req)
        {
            var ok = await _logic.ActualizarTalla(id, req);
            if (!ok) return NotFound(new { mensaje = "Talla no encontrada" });
            return NoContent();
        }
    }
}