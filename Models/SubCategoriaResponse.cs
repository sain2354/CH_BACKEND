namespace CH_BACKEND.Models
{
    public class SubCategoriaResponse
    {
        public int IdSubCategoria { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdCategoria { get; set; }
    }
}
