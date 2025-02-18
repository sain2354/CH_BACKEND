using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Logica;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionController : ControllerBase
    {
        private readonly PromocionLogica _promocionLogica;

        public PromocionController(PromocionLogica promocionLogica)
        {
            _promocionLogica = promocionLogica;
        }

        [HttpGet]
        public async Task<ActionResult<List<PromocionResponse>>> ObtenerPromociones()
        {
            var promociones = await _promocionLogica.ObtenerPromociones();
            var response = promociones.Select(p => new PromocionResponse
            {
                IdPromocion = p.IdPromocion,
                Descripcion = p.Descripcion,
                FechaInicio = p.FechaInicio, // Asegúrate de que p.FechaInicio es DateTime
                FechaFin = p.FechaFin,       // Asegúrate de que p.FechaFin es DateTime
                TipoDescuento = p.TipoDescuento,
                Descuento = p.Descuento
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PromocionResponse>> ObtenerPromocionPorId(int id)
        {
            var promocion = await _promocionLogica.ObtenerPromocionPorId(id);
            if (promocion == null) return NotFound(new { mensaje = "Promoción no encontrada" });

            var response = new PromocionResponse
            {
                IdPromocion = promocion.IdPromocion,
                Descripcion = promocion.Descripcion,
                FechaInicio = promocion.FechaInicio, // Asegúrate de que es DateTime
                FechaFin = promocion.FechaFin,       // Asegúrate de que es DateTime
                TipoDescuento = promocion.TipoDescuento,
                Descuento = promocion.Descuento
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPromocion([FromBody] PromocionRequest request)
        {
            if (request == null) return BadRequest("Datos inválidos");

            var nuevaPromocion = new Promocion
            {
                Descripcion = request.Descripcion,
                FechaInicio = request.FechaInicio, // Asegúrate de que request.FechaInicio es DateTime
                FechaFin = request.FechaFin,       // Asegúrate de que request.FechaFin es DateTime
                TipoDescuento = request.TipoDescuento,
                Descuento = request.Descuento
            };

            await _promocionLogica.AgregarPromocion(nuevaPromocion);

            return CreatedAtAction(nameof(ObtenerPromocionPorId), new { id = nuevaPromocion.IdPromocion }, nuevaPromocion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPromocion(int id, [FromBody] PromocionRequest request)
        {
            var existe = await _promocionLogica.ObtenerPromocionPorId(id);
            if (existe == null) return NotFound(new { mensaje = "Promoción no encontrada" });

            var promocionActualizada = new Promocion
            {
                IdPromocion = id,
                Descripcion = request.Descripcion,
                FechaInicio = request.FechaInicio, // Asegúrate de que es DateTime
                FechaFin = request.FechaFin,       // Asegúrate de que es DateTime
                TipoDescuento = request.TipoDescuento,
                Descuento = request.Descuento
            };

            await _promocionLogica.ActualizarPromocion(promocionActualizada);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPromocion(int id)
        {
            var existe = await _promocionLogica.ObtenerPromocionPorId(id);
            if (existe == null) return NotFound(new { mensaje = "Promoción no encontrada" });

            await _promocionLogica.EliminarPromocion(id);
            return NoContent();
        }
    }
}
