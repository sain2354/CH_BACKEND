using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CH_BACKEND.Models
{
    public class VentaRequest
    {
        public int IdUsuario { get; set; }
        public string? TipoComprobante { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;
        public string? Serie { get; set; }
        public string? NumeroComprobante { get; set; }
        public decimal? TotalIgv { get; set; }
        public decimal CostoEnvio { get; set; }

        // Al aplicar el atributo [JsonProperty] indicamos que en el JSON se enviará como "detalles"
        [JsonProperty("detalles")]
        public List<DetalleVentaRequest>? Detalles { get; set; }
    }
}
