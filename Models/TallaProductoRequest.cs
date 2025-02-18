namespace CH_BACKEND.Models
{
    public class TallaProductoRequest
    {
        public int IdProducto { get; set; }
        public int IdTalla { get; set; }
        public decimal Stock { get; set; }
    }
}
