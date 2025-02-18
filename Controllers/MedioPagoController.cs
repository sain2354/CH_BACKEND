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
                    Titular = m.Titular,
                    Pagos = m.Pagos.Select(p => new PagoResponse
                    {
                        IdPago = p.IdPago,
                        MontoPagado = p.MontoPagado,
                        FechaPago = p.FechaPago
                    }).ToList()
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
                Titular = medioPago.Titular,
                Pagos = medioPago.Pagos.Select(p => new PagoResponse
                {
                    IdPago = p.IdPago,
                    MontoPagado = p.MontoPagado,
                    FechaPago = p.FechaPago
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CrearMedioPago([FromBody] global::MedioPagoResponse request)
        {
            if (request == null) return BadRequest("Datos inválidos.");

            var medioPago = new MedioPago
            {
                Descripcion = request.Descripcion,
                Titular = request.Titular,
                Pagos = request.Pagos?.Select(p => new Pago
                {
                    IdVenta = p.IdVenta,
                    MontoPagado = p.MontoPagado,
                    FechaPago = p.FechaPago
                }).ToList()
            };

            _medioPagoLogica.CrearMedioPago(medioPago);
            return CreatedAtAction(nameof(ObtenerMedioPagoPorId), new { id = medioPago.IdMedioPago }, medioPago);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarMedioPago(int id, [FromBody] global::MedioPagoResponse request)
        {
            var medioPagoExistente = _medioPagoLogica.ObtenerMedioPagoPorId(id);
            if (medioPagoExistente == null) return NotFound();

            medioPagoExistente.Descripcion = request.Descripcion;
            medioPagoExistente.Titular = request.Titular;
            medioPagoExistente.Pagos = request.Pagos?.Select(p => new Pago
            {
                IdVenta = p.IdVenta,
                MontoPagado = p.MontoPagado,
                FechaPago = p.FechaPago
            }).ToList();

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
