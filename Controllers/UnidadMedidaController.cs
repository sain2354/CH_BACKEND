using CH_BACKEND.Logica;
using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CH_BACKEND.Models; // Asegúrate de que aquí esté ubicado tu DTO

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly UnidadMedidaLogica _unidadMedidaLogica;

        public UnidadMedidaController(UnidadMedidaLogica unidadMedidaLogica)
        {
            _unidadMedidaLogica = unidadMedidaLogica;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnidadMedidaResponse>>> ObtenerUnidadesMedida()
        {
            var unidades = await _unidadMedidaLogica.ObtenerUnidadesMedida();
            var unidadesDto = unidades.ConvertAll(u => new UnidadMedidaResponse
            {
                IdUnidadMedida = u.IdUnidadMedida,
                Descripcion = u.Descripcion
            });
            return Ok(unidadesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadMedidaResponse>> ObtenerUnidadMedidaPorId(int id)
        {
            var unidad = await _unidadMedidaLogica.ObtenerUnidadMedidaPorId(id);
            if (unidad == null) return NotFound(new { mensaje = "Unidad de Medida no encontrada" });

            var unidadDto = new UnidadMedidaResponse
            {
                IdUnidadMedida = unidad.IdUnidadMedida,
                Descripcion = unidad.Descripcion
            };
            return Ok(unidadDto);
        }
    }
}
