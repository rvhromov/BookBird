using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Genre
{
    public sealed record GenreName
    {
        private const int MaxLength = 100;
        
        public string Value { get; }

        public GenreName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Genre name cannot be empty.");
            
            if (value.Length > MaxLength)
                throw new ValidationException("Invalid genre name length.");
            
            Value = value;
        }

        public static implicit operator string(GenreName name) =>
            name.Value;

        public static implicit operator GenreName(string name) =>
            new(name);
    }
}