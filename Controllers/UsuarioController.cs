using CH_BACKEND.Logica;
using CH_BACKEND.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CH_BACKEND.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioLogica _logica;

        public UsuarioController(UsuarioLogica logica)
        {
            _logica = logica;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = await _logica.ObtenerUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            try
            {
                var usuario = await _logica.ObtenerUsuarioPorId(id);
                if (usuario == null)
                    return NotFound();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuario = await _logica.CrearUsuario(request);
                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuario = await _logica.ActualizarUsuario(id, request);
                if (usuario == null)
                    return NotFound();
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                var eliminado = await _logica.EliminarUsuario(id);
                if (!eliminado)
                    return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuario = await _logica.IniciarSesion(request.UsernameOrEmail, request.Password);
                if (usuario == null)
                    return Unauthorized("Credenciales inválidas.");

                return Ok(new
                {
                    mensaje = "Inicio de sesión exitoso",
                    usuario = new
                    {
                        usuario.IdUsuario,
                        usuario.Username,
                        usuario.NombreCompleto,
                        usuario.Email,
                        usuario.Telefono
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("googleLogin")]
        public async Task<IActionResult> LoginConGoogle([FromBody] GoogleLoginRequest request)
        {
            try
            {
                var result = await _logica.LoginConGoogle(request);
                return Ok(new
                {
                    mensaje = "Login con Google exitoso",
                    usuario = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = ex.Message });
            }
        }

        // Endpoint para sincronizar usuario (crear o actualizar) DESPUÉS de Google
        [HttpPost("sync")]
        public async Task<IActionResult> SyncUsuario([FromBody] UsuarioRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // DEBUG: Imprimir qué llega
            Console.WriteLine($"[SyncUsuario] Recibido: Email={request.Email}, NombreCompleto={request.NombreCompleto}, Telefono={request.Telefono}");

            try
            {
                var usuario = await _logica.SincronizarUsuario(request);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
