using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Invitations.Commands
{
    public sealed record AcceptInvitation(Guid InvitationId) : IRequest;
}