using Microsoft.AspNetCore.Mvc;

namespace ActividadPractica1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IMC_Controller : ControllerBase
    {
        // GET: api/imc_/calcular?peso=70&altura=1.75
        [HttpGet("calcular")]
        public IActionResult Calcular([FromQuery] double peso, [FromQuery] double metros)
        {
            if (peso <= 0 || metros <= 0)
            {
                return BadRequest(new { mensaje = "favor ingresar numero mayor a cerp" });
            }

            double kilos = peso / 2.20462;

            double resultadoImc = kilos / (metros * metros);
            string diagnostico = "";

            if (resultadoImc < 18.5) diagnostico = "Peso bajo";
            else if (resultadoImc < 25) diagnostico = "Normal";
            else if (resultadoImc < 30) diagnostico = "Sobrepeso";
            else diagnostico = "Obesidad";

            return Ok(new
            {
                pesoLibras = peso,
                alturaMetros = metros,
                imcCalculado = Math.Round(resultadoImc, 2),
                categoria = diagnostico
            });
        }
    }
}
