using Microsoft.AspNetCore.Mvc;

namespace ActividadPractica1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HolaController : ControllerBase
    {
        [HttpGet("saludo")]
        public IActionResult Saludo([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest(new { mensaje = "Nombre es obligatorio" });
            }

            return Ok(new
            {
                mensaje = $"Bienvenido/a {nombre} a API REST"
            });
        }
    }
}