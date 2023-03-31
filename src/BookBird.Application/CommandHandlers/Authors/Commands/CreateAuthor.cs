using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Authors.Commands
{
    public sealed record CreateAuthor(string FirstName, string LastName) : IRequest<Guid>;
}