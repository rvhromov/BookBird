using System.Threading.Tasks;
using BookBird.Application;
using MassTransit;
using Microsoft.AspNetCore.Http;

namespace BookBird.Infrastructure.PipelineBehaviors
{
    public sealed class MessageCorrelationIdBehavior<TMessage> : IFilter<SendContext<TMessage>> where TMessage : class
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MessageCorrelationIdBehavior(IHttpContextAccessor httpContextAccessor) => 
            _httpContextAccessor = httpContextAccessor;

        public Task Send(SendContext<TMessage> context, IPipe<SendContext<TMessage>> next)
        {
            var correlationId = _httpContextAccessor.HttpContext?.TraceIdentifier;

            if (!string.IsNullOrWhiteSpace(correlationId))
            {
                context.Headers.Set(Constants.CorrelationIdHeader, correlationId);
            }

            return next.Send(context);
        }

        public void Probe(ProbeContext context)
        {
        }
    }
}