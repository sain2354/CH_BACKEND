using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriaController : ControllerBase
    {
        private readonly SubCategoriaLogica _subCategoriaLogica;

        public SubCategoriaController(SubCategoriaLogica subCategoriaLogica)
        {
            _subCategoriaLogica = subCategoriaLogica;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubCategoriaResponse>>> ObtenerSubCategorias()
        {
            var subCategorias = await _subCategoriaLogica.ObtenerSubCategorias();
            return Ok(subCategorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoriaResponse>> ObtenerSubCategoriaPorId(int id)
        {
            var subCategoria = await _subCategoriaLogica.ObtenerSubCategoriaPorId(id);
            if (subCategoria == null) return NotFound(new { mensaje = "SubCategoría no encontrada" });

            return Ok(subCategoria);
        }

        [HttpPost]
        public async Task<IActionResult> CrearSubCategoria([FromBody] SubCategoriaRequest request)
        {
            if (request == null) return BadRequest("Datos inválidos");

            await _subCategoriaLogica.AgregarSubCategoria(request);

            return Ok(new { mensaje = "SubCategoría creada correctamente" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarSubCategoria(int id, [FromBody] SubCategoriaRequest request)
        {
            var resultado = await _subCategoriaLogica.ActualizarSubCategoria(id, request);
            if (!resultado) return NotFound(new { mensaje = "SubCategoría no encontrada" });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarSubCategoria(int id)
        {
            var resultado = await _subCategoriaLogica.EliminarSubCategoria(id);
            if (!resultado) return NotFound(new { mensaje = "SubCategoría no encontrada" });

            return NoContent();
        }
    }
}
