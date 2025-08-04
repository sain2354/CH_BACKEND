using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoLogica _productoLogica;
        private readonly PromocionLogica _promocionLogica;
        private readonly IWebHostEnvironment _env;

        public ProductoController(
            ProductoLogica productoLogica,
            PromocionLogica promocionLogica,
            IWebHostEnvironment env)
        {
            _productoLogica = productoLogica;
            _promocionLogica = promocionLogica;
            _env = env;
        }

        // GET /api/Producto?cat=0&genero=...&articulo=...&estilo=...
        [HttpGet]
        public async Task<ActionResult<List<ProductoResponse>>> ObtenerProductos(
            [FromQuery] int cat = 0,
            [FromQuery] string? genero = null,
            [FromQuery] string? articulo = null,
            [FromQuery] string? estilo = null)
        {
            var productos = await _productoLogica.ObtenerProductosPorFiltro(cat, genero, articulo, estilo);
            return Ok(productos);
        }

        // GET /api/Producto/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductoResponse>> ObtenerProductoPorId(int id)
        {
            var producto = await _productoLogica.ObtenerProductoPorId(id);
            if (producto == null)
                return NotFound(new { mensaje = "Producto no encontrado" });
            return Ok(producto);
        }

        // POST /api/Producto/createWithFile
        [HttpPost("createWithFile")]
        public async Task<IActionResult> AgregarProductoConImagen(
            [FromForm] ProductoRequest request,
            [FromForm] IFormFile imagen)
        {
            if (request == null || request.IdCategoria <= 0)
                return BadRequest(new { mensaje = "Datos inválidos" });

            if (imagen == null || imagen.Length == 0)
                return BadRequest(new { mensaje = "No se ha subido ninguna imagen." });

            // Guardar imagen
            var uploadsFolder = Path.Combine(
                _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
                "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(imagen.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
                await imagen.CopyToAsync(stream);

            request.Foto = "/" + Path.Combine("uploads", uniqueFileName).Replace("\\", "/");

            // Crear producto
            int nuevoId = await _productoLogica.AgregarProducto(request);
            var productoCreado = await _productoLogica.ObtenerProductoPorId(nuevoId);

            // Sincronizar promoción INLINE
            if (request.AsignarPromocion && request.Promocion != null)
            {
                var prom = new Promocion
                {
                    Descripcion = request.Promocion.Descripcion,
                    FechaInicio = request.Promocion.FechaInicio,
                    FechaFin = request.Promocion.FechaFin,
                    TipoDescuento = request.Promocion.TipoDescuento,
                    Descuento = request.Promocion.Descuento,
                    IdProductos = new List<Producto> { new Producto { IdProducto = nuevoId } }
                };
                await _promocionLogica.AgregarPromocion(prom);
            }

            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = nuevoId }, productoCreado);
        }

        // PUT /api/Producto/{id}
        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarProducto(
            int id,
            [FromForm] ProductoRequest request,
            [FromForm] IFormFile? imagen)
        {
            if (request == null || request.IdCategoria <= 0)
                return BadRequest(new { mensaje = "Datos inválidos" });

            // 1) Obtener el producto existente para conservar la foto si no llega nueva
            var existenteResponse = await _productoLogica.ObtenerProductoPorId(id);
            if (existenteResponse == null)
                return NotFound(new { mensaje = "Producto no encontrado" });
            request.Foto = existenteResponse.Foto;

            // 2) Si llega archivo nuevo, lo guardo y sobreescribo request.Foto
            if (imagen != null && imagen.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
                    "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(imagen.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await imagen.CopyToAsync(stream);

                request.Foto = "/" + Path.Combine("uploads", uniqueFileName).Replace("\\", "/");
            }

            // 3) Actualizar producto con request.Foto correcta
            var resultado = await _productoLogica.ActualizarProducto(id, request);
            if (!resultado)
                return NotFound(new { mensaje = "Producto no encontrado" });

            // Sincronizar promoción INLINE (igual que antes)...
            var todasPromos = await _promocionLogica.ObtenerPromociones();
            var promoExistente = todasPromos.FirstOrDefault(p => p.IdProductos.Any(x => x.IdProducto == id));
            if (request.AsignarPromocion && request.Promocion != null)
            {
                if (promoExistente == null)
                {
                    var prom = new Promocion
                    {
                        Descripcion = request.Promocion.Descripcion,
                        FechaInicio = request.Promocion.FechaInicio,
                        FechaFin = request.Promocion.FechaFin,
                        TipoDescuento = request.Promocion.TipoDescuento,
                        Descuento = request.Promocion.Descuento,
                        IdProductos = new List<Producto> { new Producto { IdProducto = id } }
                    };
                    await _promocionLogica.AgregarPromocion(prom);
                }
                else
                {
                    promoExistente.Descripcion = request.Promocion.Descripcion;
                    promoExistente.FechaInicio = request.Promocion.FechaInicio;
                    promoExistente.FechaFin = request.Promocion.FechaFin;
                    promoExistente.TipoDescuento = request.Promocion.TipoDescuento;
                    promoExistente.Descuento = request.Promocion.Descuento;
                    await _promocionLogica.ActualizarPromocion(promoExistente);
                }
            }
            else if (!request.AsignarPromocion && promoExistente != null)
            {
                await _promocionLogica.EliminarPromocion(promoExistente.IdPromocion);
            }

            var productoActualizado = await _productoLogica.ObtenerProductoPorId(id);
            return Ok(new { mensaje = "Producto actualizado correctamente", data = productoActualizado });
        }

        // ———> Nuevo método DELETE <———
        // DELETE /api/Producto/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var eliminado = await _productoLogica.EliminarProducto(id);
            if (!eliminado)
                return NotFound(new { mensaje = "Producto no encontrado" });
            return NoContent();
        }
    }
}
