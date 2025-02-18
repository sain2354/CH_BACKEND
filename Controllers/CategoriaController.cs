using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/categorias")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly CategoriaLogica _categoriaLogica;

    public CategoriaController(CategoriaLogica categoriaLogica)
    {
        _categoriaLogica = categoriaLogica;
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoriaResponse>>> ObtenerCategorias()
    {
        var categorias = await _categoriaLogica.ObtenerCategorias();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponse>> ObtenerCategoriaPorId(int id)
    {
        var categoria = await _categoriaLogica.ObtenerCategoriaPorId(id);
        if (categoria == null) return NotFound();

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult> AgregarCategoria([FromBody] CategoriaRequest request)
    {
        var creada = await _categoriaLogica.AgregarCategoria(request);
        if (!creada) return BadRequest("Error al crear la categoría.");

        return Ok("Categoría creada correctamente.");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> ActualizarCategoria(int id, [FromBody] CategoriaRequest request)
    {
        var actualizada = await _categoriaLogica.ActualizarCategoria(id, request);
        if (!actualizada) return NotFound("No se encontró la categoría.");

        return Ok("Categoría actualizada correctamente.");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> EliminarCategoria(int id)
    {
        var eliminada = await _categoriaLogica.EliminarCategoria(id);
        if (!eliminada) return NotFound("No se encontró la categoría.");

        return Ok("Categoría eliminada correctamente.");
    }
}
