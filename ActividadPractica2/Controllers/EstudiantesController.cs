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

        //GET /api/estudiantes
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

            return NoContent(); // 204 NoContent solicitado
        }
    }
}