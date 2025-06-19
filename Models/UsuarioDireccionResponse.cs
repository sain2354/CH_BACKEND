using System;

namespace CH_BACKEND.Models
{
    public class UsuarioDireccionResponse
    {
        public int IdDireccion { get; set; }
        public int IdUsuario { get; set; }
        public string Direccion { get; set; } = null!;
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public string? Referencia { get; set; }
        public DateTime FechaRegistro { get; set; }

        // Constructor que mapea desde la entidad de base de datos
        public UsuarioDireccionResponse(DBCalzadosHuancayo.UsuarioDireccion entidad)
        {
            IdDireccion = entidad.IdDireccion;
            IdUsuario = entidad.IdUsuario;
            Direccion = entidad.Direccion;
            Lat = entidad.Lat;
            Lng = entidad.Lng;
            Referencia = entidad.Referencia;
            FechaRegistro = entidad.FechaRegistro;
        }
    }
}
