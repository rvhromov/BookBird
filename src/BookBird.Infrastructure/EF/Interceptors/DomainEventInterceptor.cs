using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookBird.Infrastructure.EF.Interceptors
{
    internal sealed class DomainEventInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator _mediator;

        public DomainEventInterceptor(IMediator mediator) =>
            _mediator = mediator;

        public override InterceptionResult<int> SavingChanges
            (DbContextEventData eventData, 
            InterceptionResult<int> result)
        {
            return SavingChangesAsync(eventData, result)
                .GetAwaiter()
                .GetResult();
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, 
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new())
        {
            var dbContext = eventData.Context;

            if (dbContext is null)
            {
                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }
            
            var entities = dbContext.ChangeTracker
                .Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents is not null && e.DomainEvents.Any())
                .ToList();

            foreach (var entity in entities)
            {
                var events = entity.DomainEvents.ToArray();
                entity.ClearEvents();
                
                foreach (var domainEvent in events)
                {
                    await _mediator
                        .Publish(domainEvent, cancellationToken)
                        .ConfigureAwait(false);
                }
            }
            
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}