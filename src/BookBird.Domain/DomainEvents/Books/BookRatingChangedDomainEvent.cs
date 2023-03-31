using System;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.DomainEvents.Books
{
    public sealed record BookRatingChangedDomainEvent(Guid BookId) : IDomainEvent;
}