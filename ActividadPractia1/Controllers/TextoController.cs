using Microsoft.AspNetCore.Mvc;

namespace ActividadPractica1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextoController : ControllerBase
    {
        // GET: api/texto/contar?texto=Hola Mundo
        [HttpGet("contar")]
        public IActionResult Contar([FromQuery] string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return BadRequest(new { mensaje = "texto obligatorio" });
            }

            int totalLetras = texto.Length;

            string[] palabrasSeparadas = texto.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int totalPalabras = palabrasSeparadas.Length;

            int totalVocales = 0;
            string vocalesValidas = "aeiouAEIOU";

            foreach (char letra in texto)
            {
                if (vocalesValidas.Contains(letra))
                {
                    totalVocales++;
                }
            }

            return Ok(new
            {
                textoOriginal = texto,
                palabras = totalPalabras,
                caracteres = totalLetras,
                vocales = totalVocales
            });
        }

        // GET: api/texto/invertir?texto=odnuM aloH
        [HttpGet("invertir")]
        public IActionResult Invertir([FromQuery] string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return BadRequest(new { mensaje = "El texto es obligatorio" });
            }

            char[] letrasArreglo = texto.ToCharArray();
            Array.Reverse(letrasArreglo);
            string alReves = new string(letrasArreglo);

            return Ok(new
            {
                textoOriginal = texto,
                textoInvertido = alReves
            });
        }
    }
}