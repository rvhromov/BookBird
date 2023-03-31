using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Books.Commands
{
    public sealed record DeleteBook(Guid Id) : IRequest;
}