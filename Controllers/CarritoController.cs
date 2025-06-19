using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly CarritoLogica _carritoLogica;

        public CarritoController(CarritoLogica carritoLogica)
        {
            _carritoLogica = carritoLogica;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarritoResponse>>> ObtenerCarritos()
        {
            var carritos = await _carritoLogica.ObtenerTodos();
            return Ok(carritos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoResponse>> ObtenerCarritoPorId(int id)
        {
            var carrito = await _carritoLogica.ObtenerPorId(id);
            if (carrito == null) return NotFound("Carrito no encontrado");
            return Ok(carrito);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCarrito([FromBody] CarritoRequest request)
        {
            try
            {
                var creado = await _carritoLogica.Crear(request);
                if (!creado) return StatusCode(500, "No se pudo crear el carrito");
                return Ok("Carrito creado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCarrito(int id, [FromBody] CarritoRequest request)
        {
            try
            {
                var actualizado = await _carritoLogica.Actualizar(id, request);
                if (!actualizado) return NotFound("Carrito no encontrado o no se pudo actualizar");
                return Ok("Carrito actualizado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCarrito(int id)
        {
            try
            {
                var eliminado = await _carritoLogica.Eliminar(id);
                if (!eliminado) return NotFound("Carrito no encontrado o no se pudo eliminar");
                return Ok("Carrito eliminado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
