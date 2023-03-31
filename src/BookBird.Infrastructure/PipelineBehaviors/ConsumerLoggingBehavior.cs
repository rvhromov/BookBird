using System;
using System.Text.Json;
using System.Threading.Tasks;
using BookBird.Application;
using MassTransit;
using Serilog;

namespace BookBird.Infrastructure.PipelineBehaviors
{
    public sealed class ConsumerLoggingBehavior<TMessage> : IFilter<ConsumeContext<TMessage>> where TMessage : class
    {
        private readonly ILogger _logger;

        public ConsumerLoggingBehavior(ILogger logger) =>
            _logger = logger;

        public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
        {
            var correlationId = context.Headers.Get<string>(Constants.CorrelationIdHeader);
            
            _logger.Information("Handling the {@MessageName}, {@MessageBody}, {@DateTimeUtc}, {@CorrelationId}", 
                typeof(TMessage).Name, 
                JsonSerializer.Serialize(context.Message),
                DateTime.UtcNow,
                correlationId);

            try
            {
                await next.Send(context);
            }
            catch (Exception ex)
            {
                _logger.Error("Handle failure {@MessageName}, {@Error}, {@DateTimeUtc}, {@CorrelationId}",
                    typeof(TMessage).Name,
                    ex.GetBaseException().Message,
                    DateTime.UtcNow,
                    correlationId);

                throw;
            }
            finally
            {
                _logger.Information("Handled the {@MessageName}, {@DateTimeUtc}, {@CorrelationId}",
                    typeof(TMessage).Name, 
                    DateTime.UtcNow,
                    correlationId);
            }
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}