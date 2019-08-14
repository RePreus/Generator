using System;
using System.Threading.Tasks;
using Generator.Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Generator.API.Middleware
{
    public class GeneratorExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public GeneratorExceptionMiddleware(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (GeneratorException ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = 400;
                context.Response.ContentType = @"application/json";

                await context.Response.WriteAsync(ex.Message);

                return;
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GeneratorExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGeneratorExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GeneratorExceptionMiddleware>();
        }
    }
}
