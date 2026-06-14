namespace ActividadPractica2.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Carrera { get; set; } = string.Empty;
        public int Edad { get; set; }
        public decimal Promedio { get; set; }
        public bool Activo { get; set; }
    }
}
