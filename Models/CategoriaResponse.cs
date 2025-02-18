namespace CH_BACKEND.Models
{
    public class CategoriaResponse
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }

        public CategoriaResponse(CH_BACKEND.DBCalzadosHuancayo.Categoria categoria)
        {
            IdCategoria = categoria.IdCategoria;
            Descripcion = categoria.Descripcion;
        }
    }
}
