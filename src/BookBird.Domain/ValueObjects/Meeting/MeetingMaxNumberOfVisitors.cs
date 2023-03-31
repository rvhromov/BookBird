using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Meeting
{
    public sealed record MeetingMaxNumberOfVisitors
    {
        public int? Value { get; }

        public MeetingMaxNumberOfVisitors(int? value)
        {
            if (value < default(int))
                throw new ValidationException("Invalid value for number of meeting visitors.");
            
            Value = value;
        }
        
        public static implicit operator int?(MeetingMaxNumberOfVisitors numberOfVisitors) =>
            numberOfVisitors.Value;
        
        public static implicit operator MeetingMaxNumberOfVisitors(int? numberOfVisitors) =>
            new(numberOfVisitors);
    }
}