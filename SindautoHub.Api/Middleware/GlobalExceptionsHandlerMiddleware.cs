using System.Net;
using System.Text.Json;

namespace SindautoHub.Api.Middleware
{
    public class GlobalExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionsHandlerMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;



            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = "Ocorreu um erro inesperado. " + exception.Message,
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
