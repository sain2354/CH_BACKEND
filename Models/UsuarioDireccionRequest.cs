using System;

namespace CH_BACKEND.Models
{
    public class UsuarioDireccionRequest
    {
        public int IdUsuario { get; set; }
        public string Direccion { get; set; } = null!;
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public string? Referencia { get; set; }
        // FechaRegistro usualmente la maneja el backend al crear el registro
    }
}
