using Banco_Sol_Gestion_Financiera.Middleware;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;


namespace Banco_Sol_Gestion_Financiera.Tests.Middleware
{
    public class ExceptionMiddlewareTests
    {

        private ExceptionMiddleware CreateMiddleware(
            Exception exception)
        {
            RequestDelegate next = context =>
            {
                throw exception;
            };


            var logger =
                new Mock<ILogger<ExceptionMiddleware>>();


            return new ExceptionMiddleware(
                next,
                logger.Object
            );
        }



        [Fact]
        public async Task ShouldReturn404WhenResourceNotFound()
        {
            var context = new DefaultHttpContext();

            context.Response.Body =
                new MemoryStream();


            var middleware =
                CreateMiddleware(
                    new KeyNotFoundException(
                        "Usuario no encontrado")
                );


            await middleware.InvokeAsync(context);


            Assert.Equal(
                404,
                context.Response.StatusCode
            );


            context.Response.Body.Seek(
                0,
                SeekOrigin.Begin
            );


            var response =
                await new StreamReader(
                    context.Response.Body)
                .ReadToEndAsync();


            Assert.Contains(
                "Usuario no encontrado",
                response
            );
        }



        [Fact]
        public async Task ShouldReturn400WhenArgumentException()
        {
            var context =
                new DefaultHttpContext();


            context.Response.Body =
                new MemoryStream();


            var middleware =
                CreateMiddleware(
                    new ArgumentException(
                        "Dato inválido")
                );


            await middleware.InvokeAsync(context);


            Assert.Equal(
                400,
                context.Response.StatusCode
            );
        }



        [Fact]
        public async Task ShouldReturn500ForUnhandledException()
        {
            var context =
                new DefaultHttpContext();


            context.Response.Body =
                new MemoryStream();


            var middleware =
                CreateMiddleware(
                    new Exception()
                );


            await middleware.InvokeAsync(context);


            Assert.Equal(
                500,
                context.Response.StatusCode
            );
        }



        [Fact]
        public async Task ShouldReturn400ForValidationException()
        {
            var context =
                new DefaultHttpContext();


            context.Response.Body =
                new MemoryStream();


            var validationException =
                new ValidationException(
                    new[]
                    {
                        new ValidationFailure(
                            "Amount",
                            "Debe ser mayor a cero")
                    });


            var middleware =
                CreateMiddleware(
                    validationException
                );


            await middleware.InvokeAsync(context);


            Assert.Equal(
                400,
                context.Response.StatusCode
            );


            context.Response.Body.Seek(
                0,
                SeekOrigin.Begin
            );


            var response =
                await new StreamReader(
                    context.Response.Body)
                .ReadToEndAsync();


            Assert.Contains(
                "Amount",
                response
            );
        }
    }
}
