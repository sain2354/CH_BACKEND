using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentaLogica _ventaLogica;
        private readonly ILogger<VentaController> _logger;

        public VentaController(VentaLogica ventaLogica, ILogger<VentaController> logger)
        {
            _ventaLogica = ventaLogica;
            _logger = logger;
        }

        // Obtener todas las ventas
        [HttpGet]
        public async Task<ActionResult<List<VentaResponse>>> ObtenerVentas()
        {
            try
            {
                var ventas = await _ventaLogica.ObtenerVentas();
                return Ok(ventas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las ventas.");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // Obtener una venta por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaResponse>> ObtenerVentaPorId(int id)
        {
            try
            {
                var venta = await _ventaLogica.ObtenerVentaPorId(id);
                if (venta == null)
                    return NotFound("Venta no encontrada");

                return Ok(venta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la venta con ID {id}.");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // Agregar una nueva venta
        [HttpPost]
        public async Task<IActionResult> AgregarVenta([FromBody] VentaRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Datos inválidos");

                var ventaCreada = await _ventaLogica.AgregarVenta(request);

                if (ventaCreada == null)
                    return BadRequest("No se pudo crear la venta");

                return Ok(ventaCreada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar la venta.");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // Actualizar una venta existente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVenta(int id, [FromBody] VentaRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest("Datos inválidos");

                var resultado = await _ventaLogica.ActualizarVenta(id, request);
                if (!resultado)
                    return NotFound("Venta no encontrada");

                return Ok("Venta actualizada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la venta con ID {id}.");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        // Eliminar una venta
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVenta(int id)
        {
            try
            {
                var resultado = await _ventaLogica.EliminarVenta(id);
                if (!resultado)
                    return NotFound("Venta no encontrada");

                return Ok("Venta eliminada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la venta con ID {id}.");
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}
