using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace BookBird.Infrastructure.PipelineBehaviors
{
    internal sealed class HandlerLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : class, IRequest<TResponse>
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HandlerLoggingBehavior(ILogger logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var correlationId = _httpContextAccessor.HttpContext?.TraceIdentifier;
            
            _logger.Information("Starting request {@RequestName}, {@RequestBody}, {@DateTimeUtc}, {@CorrelationId}", 
                typeof(TRequest).Name, 
                JsonSerializer.Serialize(request),
                DateTime.UtcNow,
                correlationId);

            try
            {
                var response = await next();
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error("Request failure {@RequestName}, {@Error}, {@DateTimeUtc}, {@CorrelationId}",
                    typeof(TRequest).Name,
                    ex.GetBaseException().Message,
                    DateTime.UtcNow,
                    correlationId);

                throw;
            }
            finally
            {
                _logger.Information("Completed request {@RequestName}, {@DateTimeUtc}, {@CorrelationId}",
                    typeof(TRequest).Name, 
                    DateTime.UtcNow,
                    correlationId);
            }
        }
    }
}