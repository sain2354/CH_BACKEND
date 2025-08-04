using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly PagoLogica _pagoLogica;

        public PagoController(PagoLogica pagoLogica)
        {
            _pagoLogica = pagoLogica;
        }

        [HttpGet]
        public ActionResult<List<PagoResponse>> ObtenerPagos()
            => Ok(_pagoLogica.ObtenerPagos());

        [HttpGet("{id}")]
        public ActionResult<PagoResponse> ObtenerPagoPorId(int id)
        {
            var pago = _pagoLogica.ObtenerPagoPorId(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        [HttpPost("{idVenta}")]
        public IActionResult CrearPago(int idVenta, [FromBody] PagoRequest req)
        {
            _pagoLogica.CrearPago(idVenta, req);
            return CreatedAtAction(nameof(ObtenerPagoPorId), new { id = idVenta }, null);
        }

        [HttpPut("{idPago}/validar")]
        public IActionResult ValidarPago(int idPago)
        {
            var ok = _pagoLogica.CambiarEstadoPago(idPago, "Pago Validado");
            return ok ? NoContent() : NotFound();
        }

        [HttpPut("{idPago}/rechazar")]
        public IActionResult RechazarPago(int idPago)
        {
            var ok = _pagoLogica.CambiarEstadoPago(idPago, "Rechazado");
            return ok ? NoContent() : NotFound();
        }

        [HttpPost("{idVenta}/archivo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CrearPagoConArchivo(
            int idVenta,
            [FromForm] decimal montoPagado,
            [FromForm] DateTime fechaPago,
            [FromForm] int? idMedioPago,
            [FromForm] string? idTransaccionMP,
            [FromForm] string? estadoPago,
            [FromForm] IFormFile comprobante)
        {
            if (comprobante == null || comprobante.Length == 0)
                return BadRequest("Se debe enviar un archivo de comprobante.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "comprobantes");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(comprobante.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            await using (var stream = System.IO.File.Create(filePath))
                await comprobante.CopyToAsync(stream);

            var req = new PagoRequest
            {
                MontoPagado = montoPagado,
                FechaPago = DateOnly.FromDateTime(fechaPago),
                IdMedioPago = idMedioPago.GetValueOrDefault(),
                IdTransaccionMP = idTransaccionMP,
                EstadoPago = string.IsNullOrEmpty(estadoPago) ? "Pendiente" : estadoPago,
                ComprobanteUrl = $"/comprobantes/{uniqueFileName}"
            };

            _pagoLogica.CrearPago(idVenta, req);
            return NoContent();
        }

        [HttpPut("{idPago}")]
        public IActionResult ActualizarPago(int idPago, [FromBody] PagoRequest req)
        {
            _pagoLogica.ActualizarPago(idPago, req);
            return NoContent();
        }

        [HttpDelete("{idPago}")]
        public IActionResult EliminarPago(int idPago)
        {
            _pagoLogica.EliminarPago(idPago);
            return NoContent();
        }
    }
}
