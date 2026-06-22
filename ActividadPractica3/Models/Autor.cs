using System.ComponentModel.DataAnnotations;

namespace ActividadPractica3.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nombre requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nombre debe ocupar de 3-100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nacionalidad requerida")]
        [StringLength(50, ErrorMessage = "No puede exceder 50 caracteres")]
        public string Nacionalidad { get; set; } = string.Empty;

        [Required(ErrorMessage = "Año nacimiento requerido")]
        [Range(1500, 2100, ErrorMessage = "Debe estar entre 1500-2100")]
        public int Nacimiento { get; set; }
    }
}