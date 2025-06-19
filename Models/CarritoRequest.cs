namespace CH_BACKEND.Models
{
    public class CarritoRequest
    {
        public int IdUsuario { get; set; }
        // El frontend puede o no enviar la fecha de creación; 
        // podrías establecerla en la lógica a DateTime.Now si no la pasan.
        public DateTime? FechaCreacion { get; set; }
    }
}