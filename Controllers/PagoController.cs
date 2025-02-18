using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        {
            return Ok(_pagoLogica.ObtenerPagos());
        }

        [HttpGet("{id}")]
        public ActionResult<PagoResponse> ObtenerPagoPorId(int id)
        {
            var pago = _pagoLogica.ObtenerPagoPorId(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public IActionResult CrearPago([FromBody] PagoRequest request)
        {
            _pagoLogica.CrearPago(request);
            return CreatedAtAction(nameof(ObtenerPagoPorId), new { id = request.IdVenta }, request);
        }
    }
}
