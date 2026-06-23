using System.ComponentModel.DataAnnotations;

namespace ActividadPractica3._1.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Autor { get; set; } = string.Empty;

        public int AnioPublicacion { get; set; }

        [Required]
        [StringLength(100)]
        public string Genero { get; set; } = string.Empty;

        public int NumeroPaginas { get; set; }

        public decimal Precio { get; set; }

        public bool Disponible { get; set; }
    }
}