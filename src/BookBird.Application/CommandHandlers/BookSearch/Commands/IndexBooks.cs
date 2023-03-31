using MediatR;

namespace BookBird.Application.CommandHandlers.BookSearch.Commands
{
    public sealed record IndexBooks(int TakeForLastHours = 2) : IRequest;
}