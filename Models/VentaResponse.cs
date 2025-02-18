using System;
using CH_BACKEND.DBCalzadosHuancayo;

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
    }
}