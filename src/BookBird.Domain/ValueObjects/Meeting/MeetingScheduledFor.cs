using System;
using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Meeting
{
    public sealed record MeetingScheduledFor
    {
        public DateTime Value { get; }

        public MeetingScheduledFor(DateTime value)
        {
            if (value < DateTime.UtcNow)
                throw new ValidationException("Invalid meeting date. The date must be in a future.");
            
            Value = value;
        }

        public static implicit operator DateTime(MeetingScheduledFor date) =>
            date.Value;

        public static implicit operator MeetingScheduledFor(DateTime date) =>
            new(date);
    }
}