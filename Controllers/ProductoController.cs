using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;   // Para IWebHostEnvironment
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoLogica _productoLogica;
        private readonly IWebHostEnvironment _env; // Para saber dónde está wwwroot

        public ProductoController(ProductoLogica productoLogica, IWebHostEnvironment env)
        {
            _productoLogica = productoLogica;
            _env = env;
        }

        // GET /api/Producto?cat=0
        [HttpGet]
        public async Task<ActionResult> ObtenerProductos([FromQuery] int cat = 0)
        {
            var productos = await _productoLogica.ObtenerProductosPorCategoria(cat);

            if (productos == null || productos.Count == 0)
            {
                return Ok(new List<ProductoResponse>());
            }
            return Ok(productos);
        }

        // GET /api/Producto/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoResponse>> ObtenerProductoPorId(int id)
        {
            var producto = await _productoLogica.ObtenerProductoPorId(id);
            if (producto == null)
            {
                return NotFound(new { mensaje = "Producto no encontrado" });
            }
            return Ok(producto);
        }

        // ============================================
        // MÉTODO ACTUAL que recibe JSON con la imagen en base64 (opcional)
        // ============================================
        [HttpPost]
        public async Task<IActionResult> AgregarProducto([FromBody] ProductoRequest request)
        {
            if (request == null || request.IdCategoria <= 0)
            {
                return BadRequest(new { mensaje = "Datos inválidos" });
            }

            if (request.IdSubCategoria != null && request.IdSubCategoria <= 0)
            {
                return BadRequest(new { mensaje = "IdSubCategoria debe ser mayor a 0 si se proporciona" });
            }

            try
            {
                int nuevoId = await _productoLogica.AgregarProducto(request);

                if (nuevoId <= 0)
                {
                    return StatusCode(500, new { mensaje = "No se pudo agregar el producto" });
                }

                var productoCreado = await _productoLogica.ObtenerProductoPorId(nuevoId);
                if (productoCreado == null)
                {
                    return CreatedAtAction(nameof(ObtenerProductoPorId),
                        new { id = nuevoId },
                        new { mensaje = "Producto agregado correctamente", id = nuevoId });
                }

                return CreatedAtAction(nameof(ObtenerProductoPorId),
                    new { id = nuevoId },
                    productoCreado);
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // ============================================
        // NUEVO MÉTODO: recibe un archivo (multipart) y datos del producto
        // ============================================
        [HttpPost("createWithFile")]
        public async Task<IActionResult> AgregarProductoConArchivo(
    [FromForm] ProductoRequest request,
    IFormFile? file
)
        {
            // Validaciones mínimas
            if (request == null || request.IdCategoria <= 0)
            {
                return BadRequest(new { mensaje = "Datos inválidos" });
            }

            if (request.IdSubCategoria != null && request.IdSubCategoria <= 0)
            {
                return BadRequest(new { mensaje = "IdSubCategoria debe ser mayor a 0 si se proporciona" });
            }

            try
            {
                // 1) Determinar la ruta raíz para los archivos estáticos
                var webRoot = _env.WebRootPath;
                if (string.IsNullOrEmpty(webRoot))
                {
                    // Fallback: usar carpeta "wwwroot" dentro del ContentRoot
                    webRoot = Path.Combine(_env.ContentRootPath, "wwwroot");
                }

                // 2) Si se envía un archivo, lo guardamos en /wwwroot/uploads
                if (file != null && file.Length > 0)
                {
                    // Ruta de la carpeta uploads, usando 'webRoot'
                    var uploadsFolder = Path.Combine(webRoot, "uploads");

                    // Crear la carpeta si no existe
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    // Nombre único para el archivo
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Ruta completa del archivo
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Copiar el archivo al servidor
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Guardamos en la BD solo la ruta relativa
                    request.Foto = "/uploads/" + uniqueFileName;
                }
                else
                {
                    // Si no se envía archivo, Foto queda en null o en un placeholder si lo deseas
                    // request.Foto = "/uploads/placeholder.png";
                }

                // 3) Guardar el producto en la BD
                int nuevoId = await _productoLogica.AgregarProducto(request);

                if (nuevoId <= 0)
                {
                    return StatusCode(500, new { mensaje = "No se pudo agregar el producto" });
                }

                var productoCreado = await _productoLogica.ObtenerProductoPorId(nuevoId);
                if (productoCreado == null)
                {
                    return CreatedAtAction(nameof(ObtenerProductoPorId),
                        new { id = nuevoId },
                        new { mensaje = "Producto agregado correctamente", id = nuevoId });
                }

                return CreatedAtAction(nameof(ObtenerProductoPorId),
                    new { id = nuevoId },
                    productoCreado);
            }
            catch (Exception ex)
            {
                // Para ver más detalles, podrías loguear ex.ToString() o ex.StackTrace
                return StatusCode(500, new
                {
                    mensaje = "Error interno del servidor",
                    detalle = ex.Message
                });
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoRequest request)
        {
            if (request == null || request.IdCategoria <= 0)
            {
                return BadRequest(new { mensaje = "Datos inválidos" });
            }

            if (request.IdSubCategoria != null && request.IdSubCategoria <= 0)
            {
                return BadRequest(new { mensaje = "IdSubCategoria debe ser mayor a 0 si se proporciona" });
            }

            var resultado = await _productoLogica.ActualizarProducto(id, request);
            if (!resultado)
            {
                return NotFound(new { mensaje = "Producto no encontrado" });
            }

            var productoActualizado = await _productoLogica.ObtenerProductoPorId(id);
            return Ok(new
            {
                mensaje = "Producto actualizado correctamente",
                data = productoActualizado
            });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var resultado = await _productoLogica.EliminarProducto(id);
            if (!resultado)
            {
                return NotFound(new { mensaje = "Producto no encontrado" });
            }

            return Ok(new { mensaje = "Producto eliminado correctamente" });
        }
    }
}
