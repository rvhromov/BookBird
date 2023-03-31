using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Genre
{
    public sealed record GenreDescription
    {
        private const int MaxLength = 1000;
        
        public string Value { get; }

        public GenreDescription(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Genre description cannot be empty.");
            
            if (value.Length > MaxLength)
                throw new ValidationException("Invalid genre description length.");
            
            Value = value;
        }

        public static implicit operator string(GenreDescription description) =>
            description.Value;

        public static implicit operator GenreDescription(string description) =>
            new(description);
    }
}