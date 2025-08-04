namespace CH_BACKEND.Models
{
    public class TallaProductoRequest
    {
        public int IdProducto { get; set; }

        public int Usa { get; set; }
        public int Eur { get; set; }
        public decimal Cm { get; set; }

        public decimal Stock { get; set; }
    }
}
