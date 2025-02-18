using CH_BACKEND.DBCalzadosHuancayo;

public class UsuarioRolLogica
{
    private readonly UsuarioRolRepositorio _repositorio;

    public UsuarioRolLogica(UsuarioRolRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<List<UsuarioRol>> ObtenerTodosAsync()
    {
        return await _repositorio.ObtenerTodosAsync();
    }

    public async Task<UsuarioRol?> ObtenerPorIdAsync(int idUsuario, int idRol)
    {
        return await _repositorio.ObtenerPorIdAsync(idUsuario, idRol);
    }

    public async Task AgregarAsync(int idUsuario, int idRol, DateOnly fechaAsignacion)
    {
        var nuevoUsuarioRol = new UsuarioRol
        {
            IdUsuario = idUsuario,
            IdRol = idRol,
            FechaAsignacion = fechaAsignacion
        };
        await _repositorio.AgregarAsync(nuevoUsuarioRol);
    }

    public async Task<bool> EliminarAsync(int idUsuario, int idRol)
    {
        return await _repositorio.EliminarAsync(idUsuario, idRol);
    }
}
