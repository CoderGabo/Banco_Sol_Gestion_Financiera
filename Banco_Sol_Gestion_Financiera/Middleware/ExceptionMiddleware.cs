using System.Text.Json;
using FluentValidation;

namespace Banco_Sol_Gestion_Financiera.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }


        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case KeyNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(new
                        {
                            status = 404,
                            message = exception.Message
                        }));
                    break;

                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(new
                        {
                            status = 400,
                            errors = validationException.Errors.Select(x => new
                            {
                                field = x.PropertyName,
                                message = x.ErrorMessage
                            })
                        }));
                    break;

                case ArgumentException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(new
                        {
                            status = 400,
                            message = exception.Message
                        }));
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(new
                        {
                            status = 500,
                            message = "Ocurrió un error interno en el servidor."
                        }));
                    break;
            }
        }
    }
}
