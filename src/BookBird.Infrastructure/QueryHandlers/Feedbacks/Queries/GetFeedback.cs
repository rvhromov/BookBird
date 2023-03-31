using System;
using BookBird.Application.DTOs.Feedbacks;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Feedbacks.Queries
{
    public sealed record GetFeedback(Guid Id) : IRequest<FeedbackDto>;
}