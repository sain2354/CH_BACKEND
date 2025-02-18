using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoLogica _productoLogica;

        public ProductoController(ProductoLogica productoLogica)
        {
            _productoLogica = productoLogica;
        }

        // Obtener todos los productos
        [HttpGet]
        public async Task<ActionResult<List<ProductoResponse>>> ObtenerProductos()
        {
            var productos = await _productoLogica.ObtenerProductos();
            return Ok(productos);
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoResponse>> ObtenerProductoPorId(int id)
        {
            var producto = await _productoLogica.ObtenerProductoPorId(id);
            if (producto == null)
                return NotFound("Producto no encontrado");

            return Ok(producto);
        }

        // Agregar un nuevo producto
        [HttpPost]
        public async Task<IActionResult> AgregarProducto([FromBody] ProductoRequest request)
        {
            if (request == null)
                return BadRequest("Datos inválidos");

            await _productoLogica.AgregarProducto(request);
            return Ok("Producto agregado correctamente");
        }

        // Actualizar un producto existente
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoRequest request)
        {
            if (request == null)
                return BadRequest("Datos inválidos");

            var resultado = await _productoLogica.ActualizarProducto(id, request);
            if (!resultado)
                return NotFound("Producto no encontrado");

            return Ok("Producto actualizado correctamente");
        }

        // Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var resultado = await _productoLogica.EliminarProducto(id);
            if (!resultado)
                return NotFound("Producto no encontrado");

            return Ok("Producto eliminado correctamente");
        }
    }
}
