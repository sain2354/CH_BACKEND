using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UsuarioRolController : ControllerBase
{
    private readonly UsuarioRolLogica _usuarioRolLogica;

    public UsuarioRolController(UsuarioRolLogica usuarioRolLogica)
    {
        _usuarioRolLogica = usuarioRolLogica;
    }

    // Obtener todas las relaciones Usuario-Rol con datos relevantes
    [HttpGet]
    public async Task<ActionResult<List<UsuarioRolResponse>>> ObtenerTodos()
    {
        var relaciones = await _usuarioRolLogica.ObtenerTodosAsync();

        var resultado = relaciones.Select(ur => new UsuarioRolResponse
        {
            IdUsuario = ur.IdUsuario,
            IdRol = ur.IdRol,
            FechaAsignacion = ur.FechaAsignacion,
            NombreRol = ur.IdRolNavigation?.Nombre ?? "",
            NombreUsuario = ur.IdUsuarioNavigation?.Username ?? ""
        }).ToList();

        return Ok(resultado);
    }

    // Obtener una relación específica por ID
    [HttpGet("{idUsuario}/{idRol}")]
    public async Task<ActionResult<UsuarioRolResponse>> ObtenerPorId(int idUsuario, int idRol)
    {
        var usuarioRol = await _usuarioRolLogica.ObtenerPorIdAsync(idUsuario, idRol);
        if (usuarioRol == null) return NotFound(new { mensaje = "Relación Usuario-Rol no encontrada" });

        var resultado = new UsuarioRolResponse
        {
            IdUsuario = usuarioRol.IdUsuario,
            IdRol = usuarioRol.IdRol,
            FechaAsignacion = usuarioRol.FechaAsignacion,
            NombreRol = usuarioRol.IdRolNavigation?.Nombre ?? "",
            NombreUsuario = usuarioRol.IdUsuarioNavigation?.Username ?? ""
        };

        return Ok(resultado);
    }

    // Asignar un rol a un usuario
    [HttpPost]
    public async Task<ActionResult> Agregar([FromBody] UsuarioRolRequest request)
    {
        await _usuarioRolLogica.AgregarAsync(request.IdUsuario, request.IdRol, DateOnly.FromDateTime(DateTime.UtcNow));
        return Ok(new { mensaje = "Rol asignado correctamente al usuario" });
    }

    // Eliminar una relación Usuario-Rol
    [HttpDelete("{idUsuario}/{idRol}")]
    public async Task<ActionResult> Eliminar(int idUsuario, int idRol)
    {
        var eliminado = await _usuarioRolLogica.EliminarAsync(idUsuario, idRol);
        if (!eliminado) return NotFound(new { mensaje = "Relación Usuario-Rol no encontrada" });

        return Ok(new { mensaje = "Relación Usuario-Rol eliminada correctamente" });
    }
}