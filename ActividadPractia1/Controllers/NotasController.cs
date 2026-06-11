using Microsoft.AspNetCore.Mvc;

namespace ActividadPractica1.Controllers
{
    public class RegistroNotas
    {
        public List<int> ListaNotas { get; set; } = new List<int>();
    }

    [ApiController]
    [Route("api/[controller]")]
    public class NotasController : ControllerBase
    {
        // POST: api/notas/estadisticas
        [HttpPost("estadisticas")]
        public IActionResult ObtenerEstadisticas([FromBody] RegistroNotas datos)
        {
            if (datos == null || datos.ListaNotas == null || datos.ListaNotas.Count == 0)
            {
                return BadRequest(new { mensaje = "envie una calificación/nota" });
            }

            double promedio = datos.ListaNotas.Average();
            int mayor = datos.ListaNotas.Max();
            int menor = datos.ListaNotas.Min();

            int aprobados = datos.ListaNotas.Count(n => n >= 70);
            int reprobados = datos.ListaNotas.Count(n => n < 70);

            return Ok(new
            {
                promedioGeneral = Math.Round(promedio, 2),
                notaAlta = mayor,
                notaBaja = menor,
                totalAprobados = aprobados,
                totalReprobados = reprobados
            });
        }
    }
}
