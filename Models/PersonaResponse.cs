using System;

namespace CH_BACKEND.Response
{
    public class PersonaResponse
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? TipoPersona { get; set; }
        public string? TipoDocumento { get; set; }  // Cambio a PascalCase
        public string? NumeroDocumento { get; set; } // Cambio a PascalCase
    }
}
