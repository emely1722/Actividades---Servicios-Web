using System.ComponentModel.DataAnnotations;

namespace ActividadPractica3.Models
{
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Título requerido")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Debe ocupar entre 1-200 caracteres")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Año publicación requerido")]
        [Range(1450, 2100, ErrorMessage = "Debe estar entre 1400-2100")]
        public int AnioPublicacion { get; set; }

        [Required(ErrorMessage = "Género requerido")]
        [StringLength(50, ErrorMessage = "Debe tener dentro de 50 caracteres")]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "Número de páginas requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese número de páginas mayor a 0")]
        public int NumeroPaginas { get; set; }

        [Required(ErrorMessage = "Precio requerido")]
        [Range(0.0, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        public bool Disponible { get; set; }

        [Required(ErrorMessage = "AutorId requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ser mayor a 0")]
        public int AutorId { get; set; }
    }
}