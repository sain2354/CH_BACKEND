public class UsuarioRolResponse
{
    public int IdUsuario { get; set; }
    public int IdRol { get; set; }
    public DateOnly FechaAsignacion { get; set; }
    public string NombreRol { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
}
