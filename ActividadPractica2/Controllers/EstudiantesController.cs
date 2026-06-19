using Microsoft.AspNetCore.Mvc;
using ActividadPractica2.Models;

namespace ActividadPractica2.Controllers
{
    [ApiController]
    [Route("api/estudiantes")]
    public class EstudiantesController : ControllerBase
    {
        private static readonly List<Estudiante> _estudiantesMemoria = new List<Estudiante>
        {
            new Estudiante { Id = 1, Nombre = "Ana", Apellido = "Pérez", Correo = "ana.perez@ufhec.edu.do", Carrera = "Ingeniería de Sistemas", Edad = 20, Promedio = 85.5m, Activo = true },
            new Estudiante { Id = 2, Nombre = "Pedro", Apellido = "Martínez", Correo = "pedro.m@ufhec.edu.do", Carrera = "Contabilidad", Edad = 23, Promedio = 72.0m, Activo = true },
            new Estudiante { Id = 3, Nombre = "Lucas", Apellido = "Díaz", Correo = "lucas.d@ufhec.edu.do", Carrera = "Ingeniería de Sistemas", Edad = 21, Promedio = 64.5m, Activo = false }
        };

        //GET /api/estudiantes/ListaCompleta
        [HttpGet]
        public IActionResult ListaCompleta()
        {
            return Ok(_estudiantesMemoria);
        }

        //GET /api/estudiantes/{id}
        [HttpGet("{id:int}")]
        public IActionResult ObtenerEstudiantePorId(int id)
        {
            var alumnoId = _estudiantesMemoria.FirstOrDefault(e => e.Id == id);
            if (alumnoId == null)
            {
                return NotFound(new { mensaje = "Estudiante no existe (404 Not Found)." });
            }
            return Ok(alumnoId);
        }

        //GET /api/estudiantes/BuscarEstudiante
        [HttpGet("BuscarEstudiante")]
        public IActionResult BuscarEstudiantes([FromQuery] string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return Ok(_estudiantesMemoria);
            }

            var buscarTexto = _estudiantesMemoria.Where(e =>
                e.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase) ||
                e.Apellido.Contains(texto, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            return Ok(buscarTexto);
        }

        //GET /api/estudiantes/{carrera}
        [HttpGet("{carrera}")]
        public IActionResult ObtenerCarrera(string carrera)
        {
            var carreraEstudiante = _estudiantesMemoria.Where(e =>
                e.Carrera.Equals(carrera, StringComparison.OrdinalIgnoreCase)
            ).ToList();

            return Ok(carreraEstudiante);
        }

        //GET /api/estudiantes/aprobados?promedioMinimo=70
        [HttpGet("aprobados")]
        public IActionResult ObtenerAprobados([FromQuery] decimal promedioMinimo = 70)
        {
            var aprobados = _estudiantesMemoria.Where(e => e.Promedio >= promedioMinimo).ToList();
            return Ok(aprobados);
        }

        //POST /api/estudiantes
        [HttpPost]
        public IActionResult AgregarEstudiante([FromBody] Estudiante nuevo)
        {
            if (nuevo == null)
            {
                return BadRequest(new { mensaje = "Datos inválidos" });
            }

            int nuevoId = _estudiantesMemoria.Any() ? _estudiantesMemoria.Max(e => e.Id) + 1 : 1;
            nuevo.Id = nuevoId;

            _estudiantesMemoria.Add(nuevo);
            return CreatedAtAction(nameof(ObtenerEstudiantePorId), new { id = nuevo.Id }, nuevo);
        }

        //PUT /api/estudiantes/{id}
        [HttpPut("{id:int}")]
        public IActionResult ActualizarEstudiante(int id, [FromBody] Estudiante actualizar)
        {
            var registroExistente = _estudiantesMemoria.FirstOrDefault(e => e.Id == id);
            if (registroExistente == null)
            {
                return NotFound(new { mensaje = "Estudiante no encontrado para actualizar (404 Not Found)." });
            }

            registroExistente.Nombre = actualizar.Nombre;
            registroExistente.Apellido = actualizar.Apellido;
            registroExistente.Correo = actualizar.Correo;
            registroExistente.Carrera = actualizar.Carrera;
            registroExistente.Edad = actualizar.Edad;
            registroExistente.Promedio = actualizar.Promedio;
            registroExistente.Activo = actualizar.Activo;

            return NoContent(); // 204 NoContent
        }

        //DELETE /api/estudiantes/{id}
        [HttpDelete("{id:int}")]
        public IActionResult EliminarEstudiante(int id)
        {
            var eliminarEstudiante = _estudiantesMemoria.FirstOrDefault(e => e.Id == id);
            if (eliminarEstudiante == null)
            {
                return NotFound(new { mensaje = "Estudiante no se puede eliminar porque no existe." });
            }

            _estudiantesMemoria.Remove(eliminarEstudiante);
            return NoContent(); // 204 NoContent
        }

        //GET /api/estudiantes/estadisticas
        [HttpGet("estadisticas")]
        public IActionResult MetricasGenerales()
        {
            if (!_estudiantesMemoria.Any())
            {
                return Ok(new { totalCantidad = 0, mensaje = "No hay registro" });
            }

            return Ok(new
            {
                totalCantidad = _estudiantesMemoria.Count,
                cantidadAprobados = _estudiantesMemoria.Count(e => e.Promedio >= 70),
                cantidadReprobados = _estudiantesMemoria.Count(e => e.Promedio < 70),
                promedioGeneral = Math.Round(_estudiantesMemoria.Average(e => e.Promedio), 2),
                mejorPromedio = _estudiantesMemoria.Max(e => e.Promedio),
                peorPromedio = _estudiantesMemoria.Min(e => e.Promedio)
            });
        }

        //PUT /api/estudiantes/{id}/estado?activo=false
        [HttpPut("{id:int}/estado")]
        public IActionResult CambiarEstado(int id, [FromQuery] bool activo)
        {
            var estadoEstudiante = _estudiantesMemoria.FirstOrDefault(e => e.Id == id);
            if (estadoEstudiante == null)
            {
                return NotFound(new { mensaje = "Estudiante no encontrado" });
            }

            estadoEstudiante.Activo = activo;
            return NoContent(); // 204 NoContent
        }

        //GET /api/estudiantes/activos
        [HttpGet("activos")]
        public IActionResult ObtenerActivos()
        {
            var estudiantesActivos = _estudiantesMemoria.Where(e => e.Activo).ToList();
            return Ok(estudiantesActivos);
        }
    }
}