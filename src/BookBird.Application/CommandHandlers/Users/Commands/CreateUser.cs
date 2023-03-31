using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Users.Commands
{
    public sealed record CreateUser(string Name, string Email) : IRequest<Guid>;
}