namespace CH_BACKEND.Models
{
    public class HistorialInventarioRequest
    {
        public int IdProducto { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int? UsuarioResponsable { get; set; }
        public string? DocumentoCompra { get; set; }
        public int? IdProveedor { get; set; }
        public string? ProveedorNombre { get; set; }
        public string? ProveedorContacto { get; set; }
        public decimal? CompraTotal { get; set; }
        public DateOnly? CompraFecha { get; set; }
    }
}
