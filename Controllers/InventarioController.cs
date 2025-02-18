using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using CH_BACKEND.Logica;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioLogica _inventarioLogica;

        public InventarioController(InventarioLogica inventarioLogica)
        {
            _inventarioLogica = inventarioLogica;
        }

        [HttpGet]
        public ActionResult<List<Inventario>> ObtenerInventario([FromQuery] int? idProducto, [FromQuery] DateOnly? fecha)
        {
            List<Inventario> inventario;

            if (idProducto.HasValue && fecha.HasValue)
            {
                inventario = _inventarioLogica.FiltrarPorProductoYFecha(idProducto.Value, fecha.Value);
            }
            else
            {
                inventario = _inventarioLogica.ObtenerInventario();
            }

            return Ok(inventario);
        }


        [HttpGet("{id}")]
        public ActionResult<Inventario> ObtenerInventarioPorId(int id)
        {
            var inventario = _inventarioLogica.ObtenerInventarioPorId(id);
            if (inventario == null) return NotFound();
            return Ok(inventario);
        }

        [HttpPost]
        public IActionResult CrearInventario([FromBody] InventarioRequest request)
        {
            _inventarioLogica.CrearInventario(request);
            return CreatedAtAction(nameof(ObtenerInventarioPorId), new { id = request.IdProducto }, request);
        }
    }
}
