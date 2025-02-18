using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TallaController : ControllerBase
    {
        private readonly TallaLogica _tallaLogica;

        public TallaController(TallaLogica tallaLogica)
        {
            _tallaLogica = tallaLogica;
        }

        [HttpGet]
        public async Task<ActionResult<List<TallaResponse>>> ObtenerTallas()
        {
            var tallas = await _tallaLogica.ObtenerTallas();
            return Ok(tallas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TallaResponse>> ObtenerTallaPorId(int id)
        {
            var talla = await _tallaLogica.ObtenerTallaPorId(id);
            if (talla == null) return NotFound(new { mensaje = "Talla no encontrada" });
            return Ok(talla);
        }
    }
}
