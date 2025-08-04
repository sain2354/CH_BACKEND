using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CH_BACKEND.DBCalzadosHuancayo
{
    [Table("Talla")]
    public partial class Talla
    {
        [Key]
        [Column("id_talla")]
        public int IdTalla { get; set; }

        [Required]
        [Column("categoria")]
        [StringLength(20)]
        public string Categoria { get; set; } = null!;   // Hombre, Mujer, Infantil

        [Required]
        [Column("usa")]
        public int Usa { get; set; }

        [Required]
        [Column("eur")]
        public int Eur { get; set; }

        [Required]
        [Column("cm", TypeName = "decimal(8,2)")]
        public decimal Cm { get; set; }

        // Si tenías relaciones, reintégralas aquí (p.ej. CarritoDetalle)
        // [InverseProperty("IdTallaNavigation")]
        // public virtual ICollection<CarritoDetalle> CarritoDetalles { get; set; } = new List<CarritoDetalle>();
    }
}
