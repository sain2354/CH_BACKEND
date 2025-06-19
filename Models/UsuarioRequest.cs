using System.ComponentModel.DataAnnotations;

namespace CH_BACKEND.Models
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "El username es requerido.")]
        [StringLength(50, ErrorMessage = "El username no puede superar 50 caracteres.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [StringLength(50, ErrorMessage = "La contraseña no puede superar 50 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre completo no puede superar 100 caracteres.")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido.")]
        [StringLength(100, ErrorMessage = "El email no puede superar 100 caracteres.")]
        [EmailAddress(ErrorMessage = "El email no es válido.")]
        public string Email { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "El teléfono no puede superar 20 caracteres.")]
        public string Telefono { get; set; } = "No registrado";
    }
}
