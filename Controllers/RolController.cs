using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.AspNetCore.Mvc;
using CH_BACKEND.Logica;


[Route("api/[controller]")]
[ApiController]
public class RolController : ControllerBase
{
    private readonly RolLogica _rolLogica;

    public RolController(RolLogica rolLogica)
    {
        _rolLogica = rolLogica;
    }

    // Obtener todos los roles (Usando DTO para respuesta)
    [HttpGet]
    public async Task<ActionResult<List<RolResponse>>> ObtenerRoles()
    {
        var roles = await _rolLogica.ObtenerRolesAsync();

        var rolesDto = roles.Select(r => new RolResponse
        {
            IdRol = r.IdRol,
            Nombre = r.Nombre,
            Descripcion = r.Descripcion
        }).ToList();

        return Ok(rolesDto);
    }

    // Obtener un rol por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<RolResponse>> ObtenerRolPorId(int id)
    {
        var rol = await _rolLogica.ObtenerRolPorIdAsync(id);
        if (rol == null) return NotFound(new { mensaje = "Rol no encontrado" });

        var rolDto = new RolResponse
        {
            IdRol = rol.IdRol,
            Nombre = rol.Nombre,
            Descripcion = rol.Descripcion
        };

        return Ok(rolDto);
    }

    // Agregar un nuevo rol
    [HttpPost]
    public async Task<ActionResult> AgregarRol([FromBody] RolRequest rolDto)
    {
        await _rolLogica.AgregarRolAsync(rolDto.Nombre, rolDto.Descripcion);
        return Ok(new { mensaje = "Rol agregado exitosamente" });
    }

    // Actualizar un rol existente
    [HttpPut("{id}")]
    public async Task<ActionResult> ActualizarRol(int id, [FromBody] RolRequest rolDto)
    {
        var actualizado = await _rolLogica.ActualizarRolAsync(id, rolDto.Nombre, rolDto.Descripcion);
        if (!actualizado) return NotFound(new { mensaje = "Rol no encontrado" });

        return Ok(new { mensaje = "Rol actualizado exitosamente" });
    }

    // Eliminar un rol
    [HttpDelete("{id}")]
    public async Task<ActionResult> EliminarRol(int id)
    {
        var eliminado = await _rolLogica.EliminarRolAsync(id);
        if (!eliminado) return NotFound(new { mensaje = "Rol no encontrado" });

        return Ok(new { mensaje = "Rol eliminado exitosamente" });
    }
}
