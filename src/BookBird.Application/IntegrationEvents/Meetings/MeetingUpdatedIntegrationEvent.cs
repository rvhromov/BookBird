using System;

namespace BookBird.Application.IntegrationEvents.Meetings
{
    public sealed record MeetingUpdatedIntegrationEvent(Guid MeetingId);
}