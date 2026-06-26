using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActividadPractica3._1.Data;
using ActividadPractica3._1.Models;

namespace ActividadPractica3._1.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LibrosController(AppDbContext context)
        {
            _context = context;
        }

        //GET: /api/libros (paginado)
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos([FromQuery] int pagina = 1, [FromQuery] int cantidad = 10)
        {
            if (pagina <= 0) pagina = 1;
            if (cantidad <= 0 || cantidad > 50) cantidad = 10;

            var totalRegistros = await _context.Libros.CountAsync();

            var datos = await _context.Libros
                .Skip((pagina - 1) * cantidad)
                .Take(cantidad)
                .ToListAsync();

            return Ok(new
            {
                Total = totalRegistros,
                PaginaActual = pagina,
                CantidadPorPagina = cantidad,
                Datos = datos
            });
        }

        //GET: /api/libros/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        //POST: /api/libros
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Libro nuevoLibro)
        {
            _context.Libros.Add(nuevoLibro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoLibro.Id }, nuevoLibro);
        }

        //PUT: /api/libros/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Libro libroActualizado)
        {
            if (id != libroActualizado.Id)
            {
                return BadRequest("ID no coincide");
            }

            var existe = await _context.Libros.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            _context.Entry(libroActualizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Error en actualizar");
            }

            return NoContent();
        }

        //DELETE: /api/libros/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}