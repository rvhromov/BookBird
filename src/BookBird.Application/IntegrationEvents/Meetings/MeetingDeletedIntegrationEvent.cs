using System;

namespace BookBird.Application.IntegrationEvents.Meetings
{
    public sealed record MeetingDeletedIntegrationEvent(Guid MeetingId);
}