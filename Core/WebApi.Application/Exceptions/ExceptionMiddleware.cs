using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace WebApi.Application.Exceptions
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);
            httpContext.Response.ContentType = "applicaton/json";
            httpContext.Response.StatusCode = statusCode;

            List<string> errors = new()
            {
                $"Hata mesaji: {exception.Message}",
                $"Mesaj aciklamasi: {exception.InnerException?.ToString()}",
            };


            return httpContext.Response.WriteAsync(new ExceptionModel
            {
                Errors = errors,
                StatusCode = statusCode
            }.ToString());
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}
