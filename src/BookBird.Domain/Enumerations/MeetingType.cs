using BookBird.Domain.Exceptions;
using BookBird.Domain.Primitives;

namespace BookBird.Domain.Enumerations
{
    public class MeetingType : Enumeration
    {
        public static readonly MeetingType Open = new(0, "Open");
        public static readonly MeetingType Limited = new(1, "Limited");
        
        private MeetingType(int value, string displayName) : base(value, displayName)
        {
        }

        public MeetingType()
        {
        }

        public static void VerifyCapacity(MeetingType type, int? numberOfVisitors)
        {
            if (type.Equals(Open) && numberOfVisitors is not null)
            {
                throw new ValidationException($"Unable to set number of meeting visitors because meeting is {type.DisplayName}.");
            }
            
            if (type.Equals(Limited) && numberOfVisitors is null)
            {
                throw new ValidationException($"Number of visitors is required because meeting is {type.DisplayName}.");
            }
        }
    }
}