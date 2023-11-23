using System.Net;
using System.Text.Json;

namespace OptativoApiNet7.Middlewares
{
    public class MiManejadorDeErrores
    {
        private readonly RequestDelegate _next;

        public MiManejadorDeErrores(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Crear el objeto modelo de error
                var errorResponse = new
                {
                    message = ex.Message,
                };

                // Convertir el objeto modelo a JSON
                var json = JsonSerializer.Serialize(errorResponse);

                // Establecer el tipo de contenido de la respuesta como JSON
                context.Response.ContentType = "application/json";

                // Establecer el código de estado HTTP como 400 (Bad Request)
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Escribir el JSON en la respuesta
                await context.Response.WriteAsync(json);
            }
        }
    }
}
