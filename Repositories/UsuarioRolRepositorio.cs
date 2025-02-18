using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;

public class UsuarioRolRepositorio
{
    private readonly _DbContextCalzadosHuancayo _context;

    public UsuarioRolRepositorio(_DbContextCalzadosHuancayo context)
    {
        _context = context;
    }

    public async Task<List<UsuarioRol>> ObtenerTodosAsync()
    {
        return await _context.UsuarioRols.ToListAsync();
    }

    public async Task<UsuarioRol?> ObtenerPorIdAsync(int idUsuario, int idRol)
    {
        return await _context.UsuarioRols.FindAsync(idUsuario, idRol);
    }

    public async Task AgregarAsync(UsuarioRol usuarioRol)
    {
        _context.UsuarioRols.Add(usuarioRol);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EliminarAsync(int idUsuario, int idRol)
    {
        var usuarioRol = await _context.UsuarioRols.FindAsync(idUsuario, idRol);
        if (usuarioRol == null) return false;

        _context.UsuarioRols.Remove(usuarioRol);
        await _context.SaveChangesAsync();
        return true;
    }
}
