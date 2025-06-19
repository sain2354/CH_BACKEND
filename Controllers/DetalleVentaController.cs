using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Repositories;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly DetalleVentaRepository _detalleVentaRepositorio;

        public DetalleVentaController(DetalleVentaRepository detalleVentaRepositorio)
        {
            _detalleVentaRepositorio = detalleVentaRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<DetalleVentaResponse>>> ObtenerTodos()
        {
            var detalles = await _detalleVentaRepositorio.ObtenerTodos();

            var response = detalles.Select(d => new DetalleVentaResponse
            {
                Id = d.Id,
                IdVenta = d.IdVenta,
                IdProducto = d.IdProducto,
                Cantidad = d.Cantidad,
                Precio = d.Precio,
                Descuento = d.Descuento,
                Total = d.Total,
                IdUnidadMedida = d.IdUnidadMedida,
                Igv = d.Igv,
                // Se asume que la entidad Producto tiene la propiedad "Nombre"
                NombreProducto = d.IdProductoNavigation.Nombre
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVentaResponse>> ObtenerPorId(int id)
        {
            var detalle = await _detalleVentaRepositorio.ObtenerPorId(id);
            if (detalle == null)
                return NotFound();

            var response = new DetalleVentaResponse
            {
                Id = detalle.Id,
                IdVenta = detalle.IdVenta,
                IdProducto = detalle.IdProducto,
                Cantidad = detalle.Cantidad,
                Precio = detalle.Precio,
                Descuento = detalle.Descuento,
                Total = detalle.Total,
                IdUnidadMedida = detalle.IdUnidadMedida,
                Igv = detalle.Igv,
                NombreProducto = detalle.IdProductoNavigation.Nombre
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Crear([FromBody] DetalleVentaRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Se asigna IdVenta desde el request, ya que se utiliza para asociar el detalle a la venta principal
                var detalleVenta = new DetalleVenta
                {
                    IdVenta = request.IdVenta,
                    IdProducto = request.IdProducto,
                    Cantidad = request.Cantidad,
                    Precio = request.Precio,
                    Descuento = request.Descuento,
                    Total = request.Total,
                    IdUnidadMedida = request.IdUnidadMedida,
                    Igv = request.Igv
                };

                var creado = await _detalleVentaRepositorio.Crear(detalleVenta);
                if (!creado)
                    return BadRequest("No se pudo crear el detalle de venta.");

                var response = new DetalleVentaResponse
                {
                    Id = detalleVenta.Id,
                    IdVenta = detalleVenta.IdVenta,
                    IdProducto = detalleVenta.IdProducto,
                    Cantidad = detalleVenta.Cantidad,
                    Precio = detalleVenta.Precio,
                    Descuento = detalleVenta.Descuento,
                    Total = detalleVenta.Total,
                    IdUnidadMedida = detalleVenta.IdUnidadMedida,
                    Igv = detalleVenta.Igv,
                    NombreProducto = detalleVenta.IdProductoNavigation?.Nombre ?? string.Empty
                };

                return CreatedAtAction(nameof(ObtenerPorId), new { id = detalleVenta.Id }, response);
            }
            catch (System.Exception ex)
            {
                // Loguea la excepción y retorna un error 500
                System.Console.WriteLine($"Error al crear detalle: {ex.Message}");
                return StatusCode(500, $"Error al crear detalle: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, [FromBody] DetalleVentaRequest request)
        {
            var detalleExistente = await _detalleVentaRepositorio.ObtenerPorId(id);
            if (detalleExistente == null)
                return NotFound("No se encontró el detalle de venta.");

            // Se asigna el IdVenta recibido en el request
            detalleExistente.IdVenta = request.IdVenta;
            detalleExistente.IdProducto = request.IdProducto;
            detalleExistente.Cantidad = request.Cantidad;
            detalleExistente.Precio = request.Precio;
            detalleExistente.Descuento = request.Descuento;
            detalleExistente.Total = request.Total;
            detalleExistente.IdUnidadMedida = request.IdUnidadMedida;
            detalleExistente.Igv = request.Igv;

            var actualizado = await _detalleVentaRepositorio.Actualizar(detalleExistente);
            if (!actualizado)
                return BadRequest("No se pudo actualizar el detalle de venta.");

            return Ok("Detalle de venta actualizado correctamente.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var eliminado = await _detalleVentaRepositorio.Eliminar(id);
            if (!eliminado)
                return NotFound("No se encontró el detalle de venta.");
            return Ok("Detalle de venta eliminado correctamente.");
        }
    }
}
