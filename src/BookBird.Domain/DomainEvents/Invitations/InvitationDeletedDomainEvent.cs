using System;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.DomainEvents.Invitations
{
    public sealed record InvitationDeletedDomainEvent(Guid InvitationId) : IDomainEvent;
}