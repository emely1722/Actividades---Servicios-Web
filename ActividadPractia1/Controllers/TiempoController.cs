using Microsoft.AspNetCore.Mvc;

namespace ActividadPractica1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiempoController : ControllerBase
    {
        // GET: api/tiempo/formatear?segundos=3661
        [HttpGet("formatear")]
        public IActionResult Formatear([FromQuery] int segundos)
        {
            if (segundos < 0)
            {
                return BadRequest(new { mensaje = "no pueden ser negativos" });
            }

            int hrs = segundos / 3600;
            int mins = (segundos % 3600) / 60;
            int segs = segundos % 60;

            string reloj = $"{hrs:D2}:{mins:D2}:{segs:D2}";

            return Ok(new
            {
                horas = hrs,
                minutos = mins,
                segundos = segs,
                horaCompleta = reloj
            });
        }
    }
}