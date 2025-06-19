using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
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
        // Ahora, la lógica devuelve un objeto 'CategoriaResponse' si se creó, o null si falló
        var categoriaCreada = await _categoriaLogica.AgregarCategoria(request);
        if (categoriaCreada == null)
        {
            return BadRequest("Error al crear la categoría.");
        }

        // Devolvemos JSON con un mensaje y la data recién creada
        return Ok(new
        {
            mensaje = "Categoría creada correctamente.",
            data = categoriaCreada
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> ActualizarCategoria(int id, [FromBody] CategoriaRequest request)
    {
        // La lógica devuelve un 'CategoriaResponse' si se actualizó, o null si no existe
        var categoriaActualizada = await _categoriaLogica.ActualizarCategoria(id, request);
        if (categoriaActualizada == null)
        {
            return NotFound("No se encontró la categoría.");
        }

        return Ok(new
        {
            mensaje = "Categoría actualizada correctamente.",
            data = categoriaActualizada
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> EliminarCategoria(int id)
    {
        // La lógica devuelve un bool, true si se eliminó, false si no existe
        var eliminada = await _categoriaLogica.EliminarCategoria(id);
        if (!eliminada)
        {
            return NotFound("No se encontró la categoría.");
        }

        return Ok(new
        {
            mensaje = "Categoría eliminada correctamente."
        });
    }
}
