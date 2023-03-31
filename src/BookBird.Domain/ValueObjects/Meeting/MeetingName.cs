using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Meeting
{
    public sealed record MeetingName
    {
        private const int MaxLength = 100;
        
        public string Value { get; }
        
        public MeetingName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Meeting name cannot be empty.");
            
            if (value.Length > MaxLength)
                throw new ValidationException("Invalid meeting name length.");
            
            Value = value;
        }
        
        public static implicit operator string(MeetingName name) =>
            name.Value;
        
        public static implicit operator MeetingName(string name) =>
            new(name);
    }
}