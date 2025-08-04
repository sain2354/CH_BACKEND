namespace CH_BACKEND.Models
{
    public class TallaResponse
    {
        public int IdTalla { get; set; }
        public string Categoria { get; set; } = null!;
        public int Usa { get; set; }
        public int Eur { get; set; }
        public decimal Cm { get; set; }
    }
}
