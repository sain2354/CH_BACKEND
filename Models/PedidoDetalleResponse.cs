using System;
using System.Collections.Generic;

namespace CH_BACKEND.Models
{
    public class PedidoDetalleResponse
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;
        public string EstadoPago { get; set; } = null!;
        public decimal CostoEnvio { get; set; }

        // — Propiedades nuevas —
        public string MetodoEntrega { get; set; } = null!;
        public string? SucursalRecoge { get; set; }

        public ClienteDto Cliente { get; set; } = null!;
        public DireccionDto? DireccionEntrega { get; set; }

        public List<DetalleDto> Detalles { get; set; } = new();
        public List<PagoDto> Pagos { get; set; } = new();
    }

    public class ClienteDto
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class DireccionDto
    {
        public int IdDireccion { get; set; }
        public string Direccion { get; set; } = null!;
        public string? Referencia { get; set; }
    }

    public class DetalleDto
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string ImagenUrl { get; set; } = null!;
        public string Talla { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }
    }

    public class PagoDto
    {
        public int IdPago { get; set; }
        public int IdMedioPago { get; set; }
        public string EstadoPago { get; set; } = null!;
        public string? IdTransaccionMP { get; set; }
        public DateTime FechaPago { get; set; }
        public string? ComprobanteUrl { get; set; }
    }
}
