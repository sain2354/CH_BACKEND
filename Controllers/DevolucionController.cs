using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevolucionController : ControllerBase
    {
        private readonly DevolucionLogica _devolucionLogica;

        public DevolucionController(DevolucionLogica devolucionLogica)
        {
            _devolucionLogica = devolucionLogica;
        }

        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            return Ok(_devolucionLogica.ObtenerDevoluciones());
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var result = _devolucionLogica.ObtenerDevolucionPorId(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] DevolucionRequest request)
        {
            _devolucionLogica.CrearDevolucion(request);
            return Created("", request);
        }
    }
}
