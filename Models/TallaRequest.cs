using System.ComponentModel.DataAnnotations;

namespace CH_BACKEND.Models
{
    public class TallaRequest
    {
        [Required]
        public string Categoria { get; set; } = null!;

        [Required]
        public int Usa { get; set; }

        [Required]
        public int Eur { get; set; }

        [Required]
        public decimal Cm { get; set; }
    }
}
