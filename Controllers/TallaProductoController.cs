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
        private readonly TallaProductoLogica _tallaProductoLogica;

        public TallaProductoController(TallaProductoLogica tallaProductoLogica)
        {
            _tallaProductoLogica = tallaProductoLogica;
        }

        [HttpGet]
        public async Task<ActionResult<List<TallaProductoResponse>>> ObtenerTallaProductos()
        {
            var lista = await _tallaProductoLogica.ObtenerTallaProductos();
            return Ok(lista);
        }

        [HttpGet("{idProducto}/{idTalla}")]
        public async Task<ActionResult<TallaProductoResponse>> ObtenerTallaProductoPorId(int idProducto, int idTalla)
        {
            var tallaProducto = await _tallaProductoLogica.ObtenerTallaProductoPorId(idProducto, idTalla);
            if (tallaProducto == null) return NotFound(new { mensaje = "No encontrado" });
            return Ok(tallaProducto);
        }

        [HttpPost]
        public async Task<ActionResult> CrearTallaProducto([FromBody] TallaProductoRequest request)
        {
            var nuevo = await _tallaProductoLogica.CrearTallaProducto(request);
            return CreatedAtAction(nameof(ObtenerTallaProductoPorId), new { idProducto = nuevo.IdProducto, idTalla = nuevo.IdTalla }, nuevo);
        }

        [HttpPut("{idProducto}/{idTalla}")]
        public async Task<ActionResult> ActualizarTallaProducto(int idProducto, int idTalla, [FromBody] TallaProductoRequest request)
        {
            var resultado = await _tallaProductoLogica.ActualizarTallaProducto(idProducto, idTalla, request);
            if (!resultado) return NotFound(new { mensaje = "No encontrado" });
            return NoContent();
        }

        [HttpDelete("{idProducto}/{idTalla}")]
        public async Task<ActionResult> EliminarTallaProducto(int idProducto, int idTalla)
        {
            var resultado = await _tallaProductoLogica.EliminarTallaProducto(idProducto, idTalla);
            if (!resultado) return NotFound(new { mensaje = "No encontrado" });
            return NoContent();
        }
    }
}
