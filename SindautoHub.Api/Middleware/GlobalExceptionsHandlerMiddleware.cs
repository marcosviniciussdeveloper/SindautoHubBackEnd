using System.Net;
using System.Text.Json;
using SindautoHub.Application.Exceptions;
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

            int statusCode;
            string message;

            if (exception is BadRequestException badRequest)
            {
                statusCode = (int)HttpStatusCode.BadRequest; // 400
                message = badRequest.Message;
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError; // 500
                message = "Ocorreu um erro inesperado: " + exception.Message;
            }

            context.Response.StatusCode = statusCode;

            var response = new
            {
                statusCode = statusCode,
                message = message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
