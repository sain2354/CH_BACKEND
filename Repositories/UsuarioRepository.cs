using CH_BACKEND.DBCalzadosHuancayo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UsuarioRepository
{
    private readonly _DbContextCalzadosHuancayo _context;

    public UsuarioRepository(_DbContextCalzadosHuancayo context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> GetAllUsuarios()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> GetUsuarioById(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario?> GetUsuarioByUsername(string username)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<Usuario?> GetUsuarioByEmail(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario> AddUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> UpdateUsuario(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<bool> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null)
            return false;

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return true;
    }
}
