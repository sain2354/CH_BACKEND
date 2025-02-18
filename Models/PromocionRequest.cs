public class PromocionRequest
{
    public string Descripcion { get; set; }
    public DateOnly FechaInicio { get; set; }  // Usando DateTime
    public DateOnly FechaFin { get; set; }    // Usando DateTime
    public string TipoDescuento { get; set; }
    public decimal Descuento { get; set; }
    public List<int> IdProductos { get; set; }
}
