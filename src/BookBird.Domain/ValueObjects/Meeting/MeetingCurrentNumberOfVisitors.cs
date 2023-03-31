using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Meeting
{
    public class MeetingCurrentNumberOfVisitors
    {
        public int Value { get; }

        public MeetingCurrentNumberOfVisitors(int value)
        {
            if (value < default(int))
                throw new ValidationException("Invalid value for number of meeting visitors.");
            
            Value = value;
        }
        
        public static implicit operator int(MeetingCurrentNumberOfVisitors numberOfVisitors) =>
            numberOfVisitors.Value;
        
        public static implicit operator MeetingCurrentNumberOfVisitors(int numberOfVisitors) =>
            new(numberOfVisitors);
    }
}