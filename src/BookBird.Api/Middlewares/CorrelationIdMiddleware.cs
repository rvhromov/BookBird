using System.Threading.Tasks;
using BookBird.Application.Helpers;
using BookBird.Application.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BookBird.Api.Middlewares
{
    public class CorrelationIdMiddleware : IMiddleware
    {
        private readonly CorrelationIdOptions _correlationIdOptions;

        public CorrelationIdMiddleware(IConfiguration configuration) => 
            _correlationIdOptions = configuration.GetOptions<CorrelationIdOptions>(nameof(CorrelationIdOptions));

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // If we do not find a correlation ID on the request header
            // we just utilise the existing TraceIdentifier which is generated automatically.
            if (context.Request.Headers.TryGetValue(_correlationIdOptions.Header, out var correlationId))
            {
                context.TraceIdentifier = correlationId;
            }

            if (_correlationIdOptions.IncludeInResponse)
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add(_correlationIdOptions.Header, new[] { context.TraceIdentifier });
                    return Task.CompletedTask;
                });
            }

            return next(context);
        }
    }
}