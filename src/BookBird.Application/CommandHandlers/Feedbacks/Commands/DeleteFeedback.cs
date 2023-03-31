using System;
using MediatR;

namespace BookBird.Application.CommandHandlers.Feedbacks.Commands
{
    public sealed record DeleteFeedback(Guid Id) : IRequest;
}