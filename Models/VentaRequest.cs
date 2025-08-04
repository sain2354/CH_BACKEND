using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CH_BACKEND.Models
{
    public class VentaRequest
    {
        [JsonProperty("idUsuario")]
        public int IdUsuario { get; set; }

        [JsonProperty("tipoComprobante")]
        public string? TipoComprobante { get; set; }

        [JsonProperty("fecha")]
        public DateTime Fecha { get; set; }

        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; } = "Pendiente";

        [JsonProperty("serie")]
        public string? Serie { get; set; }

        [JsonProperty("numeroComprobante")]
        public string? NumeroComprobante { get; set; }

        [JsonProperty("totalIgv")]
        public decimal? TotalIgv { get; set; }

        [JsonProperty("costo_envio")]
        public decimal CostoEnvio { get; set; }

        // — Campos nuevos —
        [JsonProperty("metodo_entrega")]
        public string MetodoEntrega { get; set; } = "delivery";

        [JsonProperty("sucursal_recoge")]
        public string? SucursalRecoge { get; set; }

        [JsonProperty("detalles")]
        public List<DetalleVentaRequest>? Detalles { get; set; }
    }

    
}
