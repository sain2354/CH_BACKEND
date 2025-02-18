using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly DetalleVentaRepositorio _detalleVentaRepositorio;

        public DetalleVentaController(DetalleVentaRepositorio detalleVentaRepositorio)
        {
            _detalleVentaRepositorio = detalleVentaRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<DetalleVenta>>> ObtenerTodos()
        {
            var detalles = await _detalleVentaRepositorio.ObtenerTodos();
            return Ok(detalles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVenta>> ObtenerPorId(int id)
        {
            var detalle = await _detalleVentaRepositorio.ObtenerPorId(id);
            if (detalle == null) return NotFound();
            return Ok(detalle);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] DetalleVenta detalleVenta)
        {
            var creado = await _detalleVentaRepositorio.Crear(detalleVenta);
            if (!creado) return BadRequest("No se pudo crear el detalle de venta.");
            return Ok("Detalle de venta creado correctamente.");
        }

        [HttpPut]
        public async Task<ActionResult> Actualizar([FromBody] DetalleVenta detalleVenta)
        {
            var actualizado = await _detalleVentaRepositorio.Actualizar(detalleVenta);
            if (!actualizado) return BadRequest("No se pudo actualizar el detalle de venta.");
            return Ok("Detalle de venta actualizado correctamente.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminado = await _detalleVentaRepositorio.Eliminar(id);
            if (!eliminado) return NotFound("No se encontró el detalle de venta.");
            return Ok("Detalle de venta eliminado correctamente.");
        }
    }
}
