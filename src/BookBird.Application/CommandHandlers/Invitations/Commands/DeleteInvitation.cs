using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Invitations.Commands
{
    public sealed record DeleteInvitation(Guid Id) : IRequest;
}