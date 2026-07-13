using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActividadPractica3.Seguridad
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiName = "X-API-Key";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiName, out var Apikeyextraida))
            {
                context.Result = new ObjectResult(new { msg = "Falta X-API-Key para acceder" })
                {
                    StatusCode = 401
                };
                return;
            }

            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = config["ApiKey"];

            if (string.IsNullOrEmpty(apiKey) || !apiKey.Equals(Apikeyextraida.ToString()))
            {
                context.Result = new ObjectResult(new { msg = "Clave API Key incorrecta." })
                {
                    StatusCode = 401
                };
                return;
            }

            await next();
        }
    }
}