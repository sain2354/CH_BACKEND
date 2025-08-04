using System;
using CH_BACKEND.DBCalzadosHuancayo;

namespace CH_BACKEND.Models
{
    public class VentaResponse
    {
        public int IdVenta { get; set; }
        public string? TipoComprobante { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;
        public string? Serie { get; set; }
        public string? NumeroComprobante { get; set; }
        public decimal? TotalIgv { get; set; }
        public string EstadoPago { get; set; } = null!;
        public decimal CostoEnvio { get; set; }

        // NUEVOS CAMPOS
        public string MetodoEntrega { get; set; } = null!;
        public string? SucursalRecoge { get; set; }

        public VentaResponse(Venta venta)
        {
            IdVenta = venta.IdVenta;
            TipoComprobante = venta.TipoComprobante;
            Fecha = venta.Fecha;
            Total = venta.Total;
            Estado = venta.Estado;
            Serie = venta.Serie;
            NumeroComprobante = venta.NumeroComprobante;
            TotalIgv = venta.TotalIgv;
            EstadoPago = venta.EstadoPago;
            CostoEnvio = venta.CostoEnvio;
            MetodoEntrega = venta.MetodoEntrega;
            SucursalRecoge = venta.SucursalRecoge;
        }
    }
}
