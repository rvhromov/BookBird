using System;

namespace BookBird.Application.IntegrationEvents.Invitations
{
    public sealed record InvitationDeletedIntegrationEvent(Guid InvitationId);
}