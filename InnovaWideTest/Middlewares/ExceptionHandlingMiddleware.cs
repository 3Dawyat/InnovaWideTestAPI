using Newtonsoft.Json;
using Serilog;

namespace InnovaWideTest.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
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
                Log.Error(ex, $"An error occurred in the server {ex.InnerException}", ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var json = JsonConvert.SerializeObject("An error occurred in the server.");
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
                return;
            }
        }
    }
}
