namespace CH_BACKEND.Models
{
    public class DevolucionRequest
    {
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public decimal CantidadDevolucion { get; set; }
        public DateOnly Fecha { get; set; }
        public string? Motivo { get; set; }
    }
}
