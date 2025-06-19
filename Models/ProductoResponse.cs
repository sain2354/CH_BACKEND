using CH_BACKEND.DBCalzadosHuancayo;

public class ProductoResponse
{
    public int IdProducto { get; set; }
    public string Nombre { get; set; }
    public string CodigoBarra { get; set; }
    public decimal PrecioVenta { get; set; }
    public decimal Stock { get; set; }
    public decimal StockMinimo { get; set; }
    public decimal? PrecioCompra { get; set; }
    public bool Estado { get; set; }
    public string? Foto { get; set; }
    public int IdCategoria { get; set; }
    public int? IdSubCategoria { get; set; }
    public int IdUnidadMedida { get; set; }
    public string Marca { get; set; }

    // NUEVO: la lista de tallas
    public List<string> Tallas { get; set; } = new List<string>();

    public ProductoResponse() { }

    public ProductoResponse(Producto producto)
    {
        IdProducto = producto.IdProducto;
        Nombre = producto.Nombre;
        CodigoBarra = producto.CodigoBarra;
        PrecioVenta = producto.PrecioVenta;
        Stock = producto.Stock;
        StockMinimo = producto.StockMinimo;
        PrecioCompra = producto.PrecioCompra;
        Estado = producto.Estado;
        Foto = producto.Foto;
        IdCategoria = producto.IdCategoria;
        IdSubCategoria = producto.IdSubCategoria;
        IdUnidadMedida = producto.IdUnidadMedida;

        Marca = producto.IdSubCategoriaNavigation != null
            ? producto.IdSubCategoriaNavigation.Descripcion
            : "Sin marca";

        // AQUÍ convertimos la relación TallaProductos -> Tallas (descripción)
        if (producto.TallaProductos != null)
        {
            // Por cada TallaProducto, tomamos la descripción de Talla
            foreach (var tp in producto.TallaProductos)
            {
                // Chequea que IdTallaNavigation no sea null
                if (tp.IdTallaNavigation != null)
                {
                    Tallas.Add(tp.IdTallaNavigation.Descripcion);
                }
            }
        }
    }
}
