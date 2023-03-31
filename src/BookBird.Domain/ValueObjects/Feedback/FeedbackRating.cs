using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Feedback
{
    public sealed record FeedbackRating
    {
        private const int MaxRatingValue = 5;
        
        public ushort Value { get; }

        public FeedbackRating(ushort value)
        {
            if (value > MaxRatingValue)
                throw new ValidationException($"Feedback rating must be less or equal to {MaxRatingValue}");
            
            Value = value;
        }

        public static implicit operator ushort(FeedbackRating rating) =>
            rating.Value;

        public static implicit operator FeedbackRating(ushort rating) =>
            new(rating);
    }
}