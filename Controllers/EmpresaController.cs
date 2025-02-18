using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Logica;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaLogica _empresaLogic;

        public EmpresaController(EmpresaLogica empresaLogic)
        {
            _empresaLogic = empresaLogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empresa>>> ObtenerEmpresas()
        {
            return Ok(await _empresaLogic.ObtenerEmpresasAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empresa>> ObtenerEmpresaPorId(int id)
        {
            var empresa = await _empresaLogic.ObtenerEmpresaPorIdAsync(id);
            if (empresa == null)
                return NotFound("Empresa no encontrada");

            return Ok(empresa);
        }

        [HttpPost]
        public async Task<ActionResult<Empresa>> CrearEmpresa([FromBody] Empresa empresa)
        {
            var nuevaEmpresa = await _empresaLogic.CrearEmpresaAsync(empresa);
            return CreatedAtAction(nameof(ObtenerEmpresaPorId), new { id = nuevaEmpresa.IdEmpresa }, nuevaEmpresa);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Empresa>> ActualizarEmpresa(int id, [FromBody] Empresa empresa)
        {
            if (id != empresa.IdEmpresa)
                return BadRequest("ID de empresa no coincide");

            var empresaActualizada = await _empresaLogic.ActualizarEmpresaAsync(empresa);
            if (empresaActualizada == null)
                return NotFound("Empresa no encontrada");

            return Ok(empresaActualizada);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarEmpresa(int id)
        {
            var eliminado = await _empresaLogic.EliminarEmpresaAsync(id);
            if (!eliminado)
                return NotFound("Empresa no encontrada");

            return NoContent();
        }
    }
}
