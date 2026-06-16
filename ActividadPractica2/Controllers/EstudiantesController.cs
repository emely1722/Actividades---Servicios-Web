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

    }
}