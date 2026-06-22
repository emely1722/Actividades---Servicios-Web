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
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AutoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/autores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> Get()
        {
            return await _context.Autores.ToListAsync();
        }

        // GET: api/autores/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> GetPorId(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return NotFound(new { msg = "Autor no encontrado" });
            return autor;
        }

        // GET: api/autores/paginado
        [HttpGet("paginado")]
        public async Task<IActionResult> GetPaginado(
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanoPagina = 10,
            [FromQuery] string? buscar = null,
            [FromQuery] string? ordenarPor = null,
            [FromQuery] string direccion = "asc")
        {
            var consulta = _context.Autores.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                consulta = consulta.Where(a => a.Nombre.Contains(buscar) || a.Nacionalidad.Contains(buscar));
            }

            if (!string.IsNullOrEmpty(ordenarPor) && ordenarPor.Equals("AnioNacimiento", StringComparison.OrdinalIgnoreCase))
            {
                consulta = direccion.ToLower() == "desc"
                    ? consulta.OrderByDescending(a => a.AnioNacimiento)
                    : consulta.OrderBy(a => a.AnioNacimiento);
            }
            else
            {
                consulta = direccion.ToLower() == "desc"
                    ? consulta.OrderByDescending(a => a.Nombre)
                    : consulta.OrderBy(a => a.Nombre);
            }

            var total = await consulta.CountAsync();
            var datos = await consulta.Skip((pagina - 1) * tamanoPagina).Take(tamanoPagina).ToListAsync();

            return Ok(new { totalRegistros = total, pagina, tamanoPagina, datos });
        }

        // GET: api/autores/{id}/libros
        [HttpGet("{id:int}/libros")]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibrosAutor(int id)
        {
            var existe = await _context.Autores.AnyAsync(a => a.Id == id);
            if (!existe) return NotFound(new { msg = "El autor no existe" });

            var libros = await _context.Libros.Where(l => l.AutorId == id).ToListAsync();
            return Ok(libros);
        }

        // POST: api/autores
        [HttpPost]
        public async Task<ActionResult<Autor>> Post(Autor autor)
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPorId), new { id = autor.Id }, autor);
        }

        // PUT: api/autores/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Autor autor)
        {
            if (id != autor.Id) return BadRequest(new { msg = "ID no coincide" });

            var existe = await _context.Autores.AnyAsync(a => a.Id == id);
            if (!existe) return NotFound(new { msg = "Autor no encontrado" });

            _context.Entry(autor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/autores/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null) return NotFound(new { msg = "Autor no encontrado" });

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}