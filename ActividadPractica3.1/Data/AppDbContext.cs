using Microsoft.EntityFrameworkCore;
using ActividadPractica3._1.Models;

namespace ActividadPractica3._1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Libro> Libros { get; set; } = null!;
    }
}