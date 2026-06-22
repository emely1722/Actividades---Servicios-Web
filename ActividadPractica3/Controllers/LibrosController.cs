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

        // POST: api/libros
        [HttpPost]
        public async Task<ActionResult<Libro>> Post(Libro libro)
        {
            var autorexiste = await _context.Autores.AnyAsync(a => a.Id == libro.AutorId);
            if (!autorexiste) return BadRequest(new { msg = "El Autor no existe" });

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPorId), new { id = libro.Id }, libro);
        }

        // PUT: api/libros/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Libro libro)
        {
            if (id != libro.Id) return BadRequest(new { msg = "ID no coincide" });

            var existe = await _context.Libros.AnyAsync(l => l.Id == id);
            if (!existe) return NotFound(new { msg = "Libro no encontrado" });

            var autorExiste = await _context.Autores.AnyAsync(a => a.Id == libro.AutorId);
            if (!autorExiste) return BadRequest(new { msg = "El Autor no existe" });

            _context.Entry(libro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/libros/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound(new { msg = "Libro no encontrado" });

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}