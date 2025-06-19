using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CH_BACKEND.DBCalzadosHuancayo;
using CH_BACKEND.Models;
using BCrypt.Net;

public class UsuarioLogica
{
    private readonly UsuarioRepository _repository;

    public UsuarioLogica(UsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UsuarioResponse>> ObtenerUsuarios()
    {
        var usuarios = await _repository.GetAllUsuarios();
        return usuarios.Select(u => new UsuarioResponse
        {
            IdUsuario = u.IdUsuario,
            Username = u.Username,
            NombreCompleto = u.NombreCompleto,
            Email = u.Email,
            Telefono = u.Telefono,
            FechaRegistro = u.FechaRegistro
        }).ToList();
    }

    public async Task<UsuarioResponse?> ObtenerUsuarioPorId(int id)
    {
        var usuario = await _repository.GetUsuarioById(id);
        if (usuario == null)
            return null;

        return new UsuarioResponse
        {
            IdUsuario = usuario.IdUsuario,
            Username = usuario.Username,
            NombreCompleto = usuario.NombreCompleto,
            Email = usuario.Email,
            Telefono = usuario.Telefono,
            FechaRegistro = usuario.FechaRegistro
        };
    }

    // Crear usuario con contraseña encriptada
    public async Task<UsuarioResponse> CrearUsuario(UsuarioRequest request)
    {
        // Normalizamos el email
        request.Email = request.Email.Trim().ToLower();

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var usuario = new Usuario
        {
            Username = request.Username,
            Password = passwordHash,
            NombreCompleto = request.NombreCompleto,
            Email = request.Email,
            Telefono = request.Telefono,
            FechaRegistro = DateTime.Now
        };

        usuario = await _repository.AddUsuario(usuario);
        return new UsuarioResponse
        {
            IdUsuario = usuario.IdUsuario,
            Username = usuario.Username,
            NombreCompleto = usuario.NombreCompleto,
            Email = usuario.Email,
            Telefono = usuario.Telefono,
            FechaRegistro = usuario.FechaRegistro
        };
    }

    // Actualizar usuario con contraseña encriptada
    public async Task<UsuarioResponse?> ActualizarUsuario(int id, UsuarioRequest request)
    {
        var usuario = await _repository.GetUsuarioById(id);
        if (usuario == null)
            return null;

        request.Email = request.Email.Trim().ToLower();

        usuario.Username = request.Username;
        usuario.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        usuario.NombreCompleto = request.NombreCompleto;
        usuario.Email = request.Email;
        usuario.Telefono = request.Telefono;

        usuario = await _repository.UpdateUsuario(usuario);
        return new UsuarioResponse
        {
            IdUsuario = usuario.IdUsuario,
            Username = usuario.Username,
            NombreCompleto = usuario.NombreCompleto,
            Email = usuario.Email,
            Telefono = usuario.Telefono,
            FechaRegistro = usuario.FechaRegistro
        };
    }

    public async Task<bool> EliminarUsuario(int id)
    {
        return await _repository.DeleteUsuario(id);
    }

    // Método para login normal
    public async Task<Usuario?> IniciarSesion(string usernameOrEmail, string password)
    {
        usernameOrEmail = usernameOrEmail.Trim().ToLower();
        var usuario = await _repository.GetUsuarioByUsername(usernameOrEmail);
        if (usuario == null)
            usuario = await _repository.GetUsuarioByEmail(usernameOrEmail);

        if (usuario == null) return null;

        bool esValida = BCrypt.Net.BCrypt.Verify(password, usuario.Password);
        if (!esValida) return null;

        return usuario;
    }

    // Método para login con Google
    public async Task<UsuarioResponse> LoginConGoogle(GoogleLoginRequest request)
    {
        request.Email = request.Email.Trim().ToLower();
        var usuario = await _repository.GetUsuarioByEmail(request.Email);
        if (usuario == null)
        {
            var randomPassword = Guid.NewGuid().ToString().Substring(0, 8);
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(randomPassword);

            var nuevoUsuario = new Usuario
            {
                Username = request.Email,
                Password = passwordHash,
                NombreCompleto = request.DisplayName ?? "Usuario Google",
                Email = request.Email,
                Telefono = string.IsNullOrEmpty(request.Phone) ? "No registrado" : request.Phone!,
                FechaRegistro = DateTime.Now
            };

            nuevoUsuario = await _repository.AddUsuario(nuevoUsuario);

            return new UsuarioResponse
            {
                IdUsuario = nuevoUsuario.IdUsuario,
                Username = nuevoUsuario.Username,
                NombreCompleto = nuevoUsuario.NombreCompleto,
                Email = nuevoUsuario.Email,
                Telefono = nuevoUsuario.Telefono,
                FechaRegistro = nuevoUsuario.FechaRegistro
            };
        }
        else
        {
            return new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                Username = usuario.Username,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                Telefono = usuario.Telefono,
                FechaRegistro = usuario.FechaRegistro
            };
        }
    }

    // Método para sincronizar usuario después de autenticarse
    public async Task<UsuarioResponse> SincronizarUsuario(UsuarioRequest request)
    {
        // Normalizar el email
        request.Email = request.Email.Trim().ToLower();

        Console.WriteLine($"[SincronizarUsuario] Normalizado => Email={request.Email}, NombreCompleto={request.NombreCompleto}, Telefono={request.Telefono}");

        var usuarioExistente = await _repository.GetUsuarioByEmail(request.Email);
        if (usuarioExistente == null)
        {
            Console.WriteLine("[SincronizarUsuario] No existe => CrearUsuario");
            return await CrearUsuario(request);
        }
        else
        {
            Console.WriteLine("[SincronizarUsuario] Existe => Actualizar => NombreCompleto=" + request.NombreCompleto + " Telefono=" + request.Telefono);

            // No cambiamos la contraseña aquí
            usuarioExistente.NombreCompleto = request.NombreCompleto;
            usuarioExistente.Telefono = request.Telefono;

            usuarioExistente = await _repository.UpdateUsuario(usuarioExistente);

            return new UsuarioResponse
            {
                IdUsuario = usuarioExistente.IdUsuario,
                Username = usuarioExistente.Username,
                NombreCompleto = usuarioExistente.NombreCompleto,
                Email = usuarioExistente.Email,
                Telefono = usuarioExistente.Telefono,
                FechaRegistro = usuarioExistente.FechaRegistro
            };
        }
    }
}
