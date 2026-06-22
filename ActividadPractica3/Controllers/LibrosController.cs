using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActividadPractica3.Data;
using ActividadPractica3.Models;
using ActividadPractica3.Seguridad;

namespace ActividadPractica3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiKey]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> Get()
        {
            return await _context.Libros.ToListAsync();
        }

        // GET: api/libros/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> GetPorId(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound(new { msg = "Libro no encontrado" });
            return libro;
        }

        // GET: api/libros/paginado
        [HttpGet("paginado")]
        public async Task<IActionResult> GetPaginado(
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanoPagina = 10,
            [FromQuery] string? buscar = null,
            [FromQuery] string? ordenarPor = null,
            [FromQuery] string direccion = "asc")
        {
            var consulta = _context.Libros.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                consulta = consulta.Where(l => l.Titulo.Contains(buscar) || l.Genero.Contains(buscar));
            }

            if (!string.IsNullOrEmpty(ordenarPor) && ordenarPor.Equals("precio", StringComparison.OrdinalIgnoreCase))
            {
                consulta = direccion.ToLower() == "desc"
                    ? consulta.OrderByDescending(l => l.Precio)
                    : consulta.OrderBy(l => l.Precio);
            }
            else
            {
                consulta = direccion.ToLower() == "desc"
                    ? consulta.OrderByDescending(l => l.Titulo)
                    : consulta.OrderBy(l => l.Titulo);
            }

            var total = await consulta.CountAsync();
            var datos = await consulta.Skip((pagina - 1) * tamanoPagina).Take(tamanoPagina).ToListAsync();

            return Ok(new { totalRegistros = total, pagina, tamanoPagina, datos });
        }

    }
}