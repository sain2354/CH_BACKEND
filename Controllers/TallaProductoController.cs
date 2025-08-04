using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TallaProductoController : ControllerBase
    {
        private readonly TallaProductoLogica _logic;

        public TallaProductoController(TallaProductoLogica logic)
        {
            _logic = logic;
        }

        // GET /api/TallaProducto/porProducto/{idProducto}
        [HttpGet("porProducto/{idProducto}")]
        public async Task<ActionResult<List<TallaProductoResponse>>> ObtenerTallasPorProducto(int idProducto)
        {
            var tallas = await _logic.ObtenerTallasPorProducto(idProducto);
            if (tallas == null || tallas.Count == 0)
                return NotFound($"No hay tallas para el producto {idProducto}");
            return Ok(tallas);
        }

        // POST /api/TallaProducto
        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] TallaProductoRequest req)
        {
            var creado = await _logic.CrearTallaProducto(req);
            return CreatedAtAction(
                nameof(ObtenerTallasPorProducto),
                new { idProducto = creado.IdProducto },
                creado
            );
        }

        // PUT /api/TallaProducto/{idProducto}/{usa}
        [HttpPut("{idProducto}/{usa}")]
        public async Task<ActionResult> Actualizar(int idProducto, int usa, [FromBody] TallaProductoRequest req)
        {
            var ok = await _logic.ActualizarTallaProducto(idProducto, usa, req);
            if (!ok) return NotFound();
            return NoContent();
        }

        // DELETE /api/TallaProducto/{idProducto}/{usa}
        [HttpDelete("{idProducto}/{usa}")]
        public async Task<ActionResult> Eliminar(int idProducto, int usa)
        {
            var ok = await _logic.EliminarTallaProducto(idProducto, usa);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
