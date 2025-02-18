using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialInventarioController : ControllerBase
    {
        private readonly HistorialInventarioLogica _historialInventarioLogica;

        public HistorialInventarioController(HistorialInventarioLogica historialInventarioLogica)
        {
            _historialInventarioLogica = historialInventarioLogica;
        }

        [HttpGet]
        public ActionResult<List<HistorialInventario>> ObtenerHistorial()
        {
            var historial = _historialInventarioLogica.ObtenerHistorial(); // Renombrado
            return Ok(historial);
        }

        [HttpGet("{id}")]
        public ActionResult<HistorialInventario> ObtenerHistorialPorId(int id)
        {
            var historial = _historialInventarioLogica.ObtenerHistorialPorId(id);
            if (historial == null) return NotFound();
            return Ok(historial);
        }

        [HttpPost]
        public IActionResult CrearHistorial([FromBody] HistorialInventarioRequest request) // Cambio de tipo de dato
        {
            _historialInventarioLogica.CrearHistorial(request);
            return CreatedAtAction(nameof(ObtenerHistorialPorId), new { id = request.IdProducto }, request);
        }
    }
}
