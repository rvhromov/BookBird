using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Meeting
{
    public sealed record MeetingLocation
    {
        private const int MaxLength = 200;
        
        public string Value { get; }
        
        public MeetingLocation(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Meeting location cannot be empty.");
            
            if (value.Length > MaxLength)
                throw new ValidationException("Invalid meeting location length.");
            
            Value = value;
        }
        
        public static implicit operator string(MeetingLocation location) =>
            location.Value;
        
        public static implicit operator MeetingLocation(string location) =>
            new(location);
    }
}