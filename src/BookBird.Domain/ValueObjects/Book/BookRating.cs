using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Book
{
    public sealed record BookRating
    {
        public double Value { get; }
        
        public BookRating(double value)
        {
            if (value < default(double))
                throw new ValidationException("Book rating cannot be negative.");
            
            Value = value;
        }

        public static implicit operator double(BookRating rating) =>
            rating.Value;

        public static implicit operator BookRating(double rating) =>
            new(rating);
    }
}