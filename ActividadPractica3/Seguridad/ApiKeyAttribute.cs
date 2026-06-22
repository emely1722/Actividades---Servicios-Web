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
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    Content = "Falta X-API-Key para acceder"
                };
                return;
            }

            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = config["ApiKey"];

            if (string.IsNullOrEmpty(apiKey) || !apiKey.Equals(Apikeyextraida))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    Content = "Clave API Key incorrecta."
                };
                return;
            }

            await next();
        }
    }
}