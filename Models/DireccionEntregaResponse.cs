using System;
using CH_BACKEND.DBCalzadosHuancayo;

namespace CH_BACKEND.Models
{
    public class DireccionEntregaResponse
    {
        public int IdDireccionEntrega { get; set; }
        public int IdVenta { get; set; }
        public string Direccion { get; set; } = null!;
        public decimal? Lat { get; set; }
        public decimal? Lng { get; set; }
        public string? Referencia { get; set; }
        public decimal CostoEnvio { get; set; }

        public DireccionEntregaResponse(DireccionEntrega entidad)
        {
            IdDireccionEntrega = entidad.IdDireccionEntrega;
            IdVenta = entidad.IdVenta;
            Direccion = entidad.Direccion;
            Lat = entidad.Lat;
            Lng = entidad.Lng;
            Referencia = entidad.Referencia;
            CostoEnvio = entidad.CostoEnvio;
        }
    }
}
