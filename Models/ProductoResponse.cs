using CH_BACKEND.DBCalzadosHuancayo;

public class ProductoResponse
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; }
    public decimal PrecioVenta { get; set; }
    public decimal Stock { get; set; }
    public bool Estado { get; set; }
    public string? Foto { get; set; }

    public ProductoResponse(Producto producto)
    {
        IdProducto = producto.IdProducto;
        Nombre = producto.Nombre;
        PrecioVenta = producto.PrecioVenta;
        Stock = producto.Stock;
        Estado = producto.Estado;
        Foto = producto.Foto;
    }
}
