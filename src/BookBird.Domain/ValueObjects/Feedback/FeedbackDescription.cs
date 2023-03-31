using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Feedback
{
    public sealed record FeedbackDescription
    {
        private const int MaxLength = 1000;
        
        public string Value { get; }

        public FeedbackDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Feedback required.");

            if (value.Length > MaxLength)
                throw new ValidationException($"Feedback length must be less than {MaxLength} characters");
            
            Value = value;
        }

        public static implicit operator string(FeedbackDescription description) =>
            description.Value;

        public static implicit operator FeedbackDescription(string description) =>
            new(description);
    }
}