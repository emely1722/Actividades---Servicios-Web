using Microsoft.AspNetCore.Mvc;

namespace ActividadPractica1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FechasController : ControllerBase
    {
        // GET: api/fechas/diferencia?desde=2020-01-01&hasta=2026-05-13
        [HttpGet("diferencia")]
        public IActionResult Diferencia([FromQuery] DateTime inicio, [FromQuery] DateTime fin)
        {
            TimeSpan rango = fin - inicio;
            int totalDias = Math.Abs(rango.Days);

            return Ok(new
            {
                fechaInicial = inicio.ToString("yyyy-MM-dd"),
                fechaFinal = fin.ToString("yyyy-MM-dd"),
                diasDiferencia = totalDias
            });
        }

        // GET: api/fechas/agregar?fecha=2026-01-01&dias=30
        [HttpGet("agregar")]
        public IActionResult Agregar([FromQuery] DateTime fechaBase, [FromQuery] int cantidadDias)
        {
            DateTime resultado = fechaBase.AddDays(cantidadDias);

            return Ok(new
            {
                fechaOriginal = fechaBase.ToString("yyyy-MM-dd"),
                diasAgregados = cantidadDias,
                fechaFinal = resultado.ToString("yyyy-MM-dd")
            });
        }
    }
}
