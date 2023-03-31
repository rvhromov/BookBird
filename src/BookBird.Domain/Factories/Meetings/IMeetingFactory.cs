using BookBird.Domain.Entities;
using BookBird.Domain.Enumerations;
using BookBird.Domain.Enums;
using BookBird.Domain.ValueObjects.Meeting;

namespace BookBird.Domain.Factories.Meetings
{
    public interface IMeetingFactory
    {
        Meeting Create(
            MeetingName name, 
            MeetingLocation location, 
            MeetingScheduledFor scheduledFor, 
            MeetingType type,
            MeetingMaxNumberOfVisitors maxNumberOfVisitors,
            User owner);
    }
}