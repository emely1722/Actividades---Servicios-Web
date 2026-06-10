using Microsoft.AspNetCore.Mvc;

namespace ActividadPractica1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumerosController : ControllerBase
    {
        [HttpGet("analizar")]
        public IActionResult Analizar([FromQuery] int numero)
        {
            string paridad = (numero % 2 == 0) ? "es par" : "es impar";

            string signo = "cero";
            if (numero > 0) signo = "positivo";
            else if (numero < 0) signo = "negativo";

            bool esPrimo = true;
            if (numero <= 1) esPrimo = false;
            for (int i = 2; i <= Math.Sqrt(Math.Abs(numero)); i++)
            {
                if (numero % i == 0)
                {
                    esPrimo = false;
                    break;
                }
            }

            return Ok(new
            {
                numero = numero,
                paridad = paridad,
                signo = signo,
                esPrimo = esPrimo
            });
        }
    }
}