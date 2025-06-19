using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedioPagoController : ControllerBase
    {
        private readonly MedioPagoLogica _medioPagoLogica;

        public MedioPagoController(MedioPagoLogica medioPagoLogica)
        {
            _medioPagoLogica = medioPagoLogica;
        }

        [HttpGet]
        public ActionResult<List<MedioPagoResponse>> ObtenerMediosPago()
        {
            var mediosPago = _medioPagoLogica.ObtenerMediosPago()
                .Select(m => new MedioPagoResponse
                {
                    IdMedioPago = m.IdMedioPago,
                    Descripcion = m.Descripcion,
                    Titular = m.Titular
                }).ToList();

            return Ok(mediosPago);
        }

        [HttpGet("{id}")]
        public ActionResult<MedioPagoResponse> ObtenerMedioPagoPorId(int id)
        {
            var medioPago = _medioPagoLogica.ObtenerMedioPagoPorId(id);
            if (medioPago == null) return NotFound();

            var response = new MedioPagoResponse
            {
                IdMedioPago = medioPago.IdMedioPago,
                Descripcion = medioPago.Descripcion,
                Titular = medioPago.Titular
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CrearMedioPago([FromBody] MedioPagoRequest request)
        {
            if (request == null) return BadRequest("Datos inválidos.");

            var medioPago = new MedioPago
            {
                Descripcion = request.Descripcion,
                Titular = request.Titular
            };

            _medioPagoLogica.CrearMedioPago(medioPago);
            return CreatedAtAction(nameof(ObtenerMedioPagoPorId), new { id = medioPago.IdMedioPago }, medioPago);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarMedioPago(int id, [FromBody] MedioPagoRequest request)
        {
            var medioPagoExistente = _medioPagoLogica.ObtenerMedioPagoPorId(id);
            if (medioPagoExistente == null) return NotFound();

            medioPagoExistente.Descripcion = request.Descripcion;
            medioPagoExistente.Titular = request.Titular;

            _medioPagoLogica.ActualizarMedioPago(medioPagoExistente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarMedioPago(int id)
        {
            _medioPagoLogica.EliminarMedioPago(id);
            return NoContent();
        }
    }
}
