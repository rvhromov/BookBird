using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Users.Commands
{
    public sealed record DeleteUser(Guid Id) : IRequest;
}