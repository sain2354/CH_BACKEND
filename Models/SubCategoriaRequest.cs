namespace CH_BACKEND.Models // Mueve la clase a otro namespace
{
    public class SubCategoriaRequest
    {
        public string Descripcion { get; set; } = null!;
        public int IdCategoria { get; set; }
    }
}
