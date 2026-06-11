using Microsoft.AspNetCore.Mvc;

namespace ActividadPractica1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropinaController : ControllerBase
    {
        // GET: api/propina/calcular?monto=1000&porcentaje=10
        [HttpGet("calcular")]
        public IActionResult Calcular([FromQuery] double monto, [FromQuery] double porcentaje)
        {
            if (monto < 0 || porcentaje < 0)
            {
                return BadRequest(new { mensaje = "no puede ingresar numeros menor a cerp" });
            }

            double propina = monto * (porcentaje / 100);
            double cuenta = monto + propina;

            return Ok(new
            {
                subtotal = monto,
                calduloPropina = propina,
                totalPago = cuenta
            });
        }
    }
}