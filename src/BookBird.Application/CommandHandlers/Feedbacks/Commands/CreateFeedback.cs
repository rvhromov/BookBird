using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Feedbacks.Commands
{
    public sealed record CreateFeedback(string Description, ushort Rating, Guid BookId) : IRequest<Guid>;
}