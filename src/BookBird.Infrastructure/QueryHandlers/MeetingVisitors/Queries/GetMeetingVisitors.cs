using System;
using System.Text.Json.Serialization;
using BookBird.Application.DTOs.MeetingVisitors;
using BookBird.Application.Helpers;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.MeetingVisitors.Queries
{
    public sealed record GetMeetingVisitors(int Skip, int Take) : IRequest<IPaginatedList<MeetingVisitorDto>>
    {
        [JsonIgnore]
        public Guid MeetingId { get; init; }
    }
}