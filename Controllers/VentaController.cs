// CH_BACKEND/Controllers/VentaController.cs
using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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

        // Método auxiliar para extraer la InnerException más profunda
        private string ObtenerMensajeExcepcionCompleto(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex.Message;
        }

        // GET api/Venta
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
                var detalle = ObtenerMensajeExcepcionCompleto(ex);
                return StatusCode(500, $"Error interno: {detalle}");
            }
        }

        // GET api/Venta/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaResponse>> ObtenerVentaPorId(int id)
        {
            try
            {
                var venta = await _ventaLogica.ObtenerVentaPorId(id);
                if (venta == null) return NotFound("Venta no encontrada");
                return Ok(venta);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener la venta {id}.");
                var detalle = ObtenerMensajeExcepcionCompleto(ex);
                return StatusCode(500, $"Error interno: {detalle}");
            }
        }

        // ** NUEVO ** GET api/Venta/{id}/detalle
        [HttpGet("{id}/detalle")]
        public async Task<IActionResult> ObtenerDetalleVenta(int id)
        {
            try
            {
                var detalle = await _ventaLogica.ObtenerPedidoDetalleCompleto(id, Request);
                if (detalle == null)
                    return NotFound($"No se encontró detalle para la venta {id}");
                return Ok(detalle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalle de la venta {id}.");
                var mensaje = ObtenerMensajeExcepcionCompleto(ex);
                return StatusCode(500, $"Error interno: {mensaje}");
            }
        }

        // POST api/Venta
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
                var detalle = ObtenerMensajeExcepcionCompleto(ex);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"Error interno al guardar en BD: {detalle}"
                );
            }
        }

        // PUT api/Venta/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVenta(int id, [FromBody] VentaRequest request)
        {
            try
            {
                if (request == null) return BadRequest("Datos inválidos");
                var ok = await _ventaLogica.ActualizarVenta(id, request);
                if (!ok) return NotFound("Venta no encontrada");
                return Ok("Venta actualizada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar venta {id}.");
                var detalle = ObtenerMensajeExcepcionCompleto(ex);
                return StatusCode(500, $"Error interno: {detalle}");
            }
        }

        // DELETE api/Venta/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVenta(int id)
        {
            try
            {
                var ok = await _ventaLogica.EliminarVenta(id);
                if (!ok) return NotFound("Venta no encontrada");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar venta {id}.");
                var detalle = ObtenerMensajeExcepcionCompleto(ex);
                return StatusCode(500, $"Error interno: {detalle}");
            }
        }

        // PUT api/Venta/{id}/pago
        [HttpPut("{id}/pago")]
        public async Task<IActionResult> PutPago(int id, [FromBody] PagoRequest req)
        {
            var ok = await _ventaLogica.AñadirPagoAsync(id, req);
            if (!ok) return NotFound("Venta no encontrada");
            return NoContent();
        }

        // POST api/Venta/{id}/pago (multipart)
        [HttpPost("{id}/pago")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PostPagoArchivo(
            int id,
            [FromForm] decimal montoPagado,
            [FromForm] DateTime fechaPago,
            [FromForm] int idMedioPago,
            [FromForm] string idTransaccionMP,
            [FromForm] string estadoPago,
            [FromForm] IFormFile comprobante)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "comprobantes");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(comprobante.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = System.IO.File.Create(filePath))
                await comprobante.CopyToAsync(stream);

            var pagoReq = new PagoRequest
            {
                MontoPagado = montoPagado,
                FechaPago = DateOnly.FromDateTime(fechaPago),
                IdMedioPago = idMedioPago,
                IdTransaccionMP = idTransaccionMP,
                EstadoPago = estadoPago,
                ComprobanteUrl = $"/comprobantes/{fileName}"
            };

            var result = await _ventaLogica.AñadirPagoAsync(id, pagoReq);
            if (!result) return NotFound("Venta no encontrada");
            return NoContent();
        }

        // PUT api/Venta/{id}/estado
        [HttpPut("{id}/estado")]
        public async Task<IActionResult> PutEstado(int id, [FromBody] EstadoRequest req)
        {
            var ok = await _ventaLogica.CambiarEstadoAsync(id, req);
            if (!ok) return NotFound("Venta no encontrada");
            return NoContent();
        }
    }
}
