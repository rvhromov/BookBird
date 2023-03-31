using BookBird.Domain.Entities;
using BookBird.Domain.Enumerations;
using BookBird.Domain.ValueObjects.Meeting;

namespace BookBird.Domain.Factories.Meetings
{
    public sealed class MeetingFactory : IMeetingFactory
    {
        public Meeting Create(
            MeetingName name, 
            MeetingLocation location, 
            MeetingScheduledFor scheduledFor, 
            MeetingType type,
            MeetingMaxNumberOfVisitors maxNumberOfVisitors, 
            User owner)
        {
            MeetingType.VerifyCapacity(type, maxNumberOfVisitors);

            return new Meeting(name, location, scheduledFor, type, maxNumberOfVisitors, default(int), owner);
        }
    }
}