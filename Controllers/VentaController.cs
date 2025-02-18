using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentaLogica _ventaLogica;

        public VentaController(VentaLogica ventaLogica)
        {
            _ventaLogica = ventaLogica;
        }

        // Obtener todas las ventas
        [HttpGet]
        public async Task<ActionResult<List<VentaResponse>>> ObtenerVentas()
        {
            var ventas = await _ventaLogica.ObtenerVentas();
            return Ok(ventas);
        }

        // Obtener una venta por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaResponse>> ObtenerVentaPorId(int id)
        {
            var venta = await _ventaLogica.ObtenerVentaPorId(id);
            if (venta == null)
                return NotFound("Venta no encontrada");

            return Ok(venta);
        }

        // Agregar una nueva venta
        [HttpPost]
        public async Task<IActionResult> AgregarVenta([FromBody] VentaRequest request)
        {
            if (request == null)
                return BadRequest("Datos inválidos");

            await _ventaLogica.AgregarVenta(request);
            return Ok("Venta agregada correctamente");
        }

        // Actualizar una venta existente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVenta(int id, [FromBody] VentaRequest request)
        {
            if (request == null)
                return BadRequest("Datos inválidos");

            var resultado = await _ventaLogica.ActualizarVenta(id, request);
            if (!resultado)
                return NotFound("Venta no encontrada");

            return Ok("Venta actualizada correctamente");
        }

        // Eliminar una venta
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVenta(int id)
        {
            var resultado = await _ventaLogica.EliminarVenta(id);
            if (!resultado)
                return NotFound("Venta no encontrada");

            return Ok("Venta eliminada correctamente");
        }
    }
}
