// InventarioRequest.cs
namespace CH_BACKEND.Models
{
    public class InventarioRequest
    {
        public int IdProducto { get; set; }
        public decimal CantidadInventario { get; set; }
        public DateOnly FechaRegistro { get; set; }
        public string TipoMovimiento { get; set; } = null!;
    }
}
