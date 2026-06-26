namespace ActividadPractica3._1.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public string Genero { get; set; } = string.Empty;
        public int NumeroPaginas { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
    }
}