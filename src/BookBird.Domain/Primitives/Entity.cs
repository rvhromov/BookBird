using System;
using System.Collections.Generic;
using BookBird.Domain.Enums;

namespace BookBird.Domain.Primitives
{
    public abstract class Entity
    {
        protected Entity()
        {  
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ModifiedAt { get; protected set; }
        public Status Status { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        
        protected void SetModificationDate(DateTime utcDate) => 
            ModifiedAt = utcDate;

        internal void AddEvent(IDomainEvent @event) => 
            _domainEvents.Add(@event);
        
        public void ClearEvents() => 
            _domainEvents?.Clear();
    }
}