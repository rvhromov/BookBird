using System;
using System.Text.Json;
using System.Threading.Tasks;
using BookBird.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BookBird.Api.Middlewares
{
    internal sealed class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var statusCode = GetStatusCode(exception);
                
                context.Response.StatusCode = statusCode;
                context.Response.Headers.Add("content-type", "application/json");
                
                var json = JsonSerializer.Serialize(new {ErrorCode = statusCode, exception.Message});
                await context.Response.WriteAsync(json);
            }
        }
        
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}