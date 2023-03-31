using System;

namespace BookBird.Application.IntegrationEvents.Invitations
{
    public sealed record InvitationAcceptedIntegrationEvent(Guid InvitationId);
}