using System;

namespace BookBird.Application.IntegrationEvents.Invitations
{
    public sealed record InvitationCreatedIntegrationEvent(Guid InvitationId);
}