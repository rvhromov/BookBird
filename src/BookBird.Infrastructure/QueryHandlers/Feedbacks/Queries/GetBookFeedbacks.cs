using System;
using System.Text.Json.Serialization;
using BookBird.Application.DTOs.Feedbacks;
using BookBird.Application.Helpers;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Feedbacks.Queries
{
    public sealed record GetBookFeedbacks(int Skip, int Take) : IRequest<IPaginatedList<FeedbackDto>>
    {
        [JsonIgnore]
        public Guid BookId { get; init; }
    }
}