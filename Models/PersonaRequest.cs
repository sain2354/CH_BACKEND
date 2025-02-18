using System;
using System.ComponentModel.DataAnnotations;

namespace CH_BACKEND.Request
{
    public class PersonaRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(255, ErrorMessage = "El nombre no debe superar los 255 caracteres.")]
        public string Nombre { get; set; } = null!;

        [StringLength(15, ErrorMessage = "El teléfono no debe superar los 15 caracteres.")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        [StringLength(255, ErrorMessage = "El correo no debe superar los 255 caracteres.")]
        public string? Correo { get; set; }

        [StringLength(255, ErrorMessage = "La dirección no debe superar los 255 caracteres.")]
        public string? Direccion { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [StringLength(50, ErrorMessage = "El tipo de persona no debe superar los 50 caracteres.")]
        public string? TipoPersona { get; set; }

        [StringLength(50, ErrorMessage = "El tipo de documento no debe superar los 50 caracteres.")]
        public string? TipoDocumento { get; set; } // Cambio a PascalCase

        [StringLength(15, ErrorMessage = "El número de documento no debe superar los 15 caracteres.")]
        public string? NumeroDocumento { get; set; } // Cambio a PascalCase
    }
}
