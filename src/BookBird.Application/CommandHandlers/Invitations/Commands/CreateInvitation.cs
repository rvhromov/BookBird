using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Invitations.Commands
{
    public sealed record CreateInvitation(Guid MeetingId, Guid UserId) : IRequest<Guid>;
}