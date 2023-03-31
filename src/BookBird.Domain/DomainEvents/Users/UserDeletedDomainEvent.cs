using System;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.DomainEvents.Users
{
    public sealed record UserDeletedDomainEvent(Guid UserId) : IDomainEvent;
}