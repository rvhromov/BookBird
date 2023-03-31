using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Author
{
    public sealed record AuthorFirstName
    {
        private const int MaxLength = 50;
        
        public string Value { get; }
        
        public AuthorFirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Author first name cannot be empty.");

            if (value.Length > MaxLength)
                throw new ValidationException("Invalid author first name length.");
            
            Value = value;
        }
        
        public static implicit operator string(AuthorFirstName firstName) =>
            firstName.Value;

        public static implicit operator AuthorFirstName(string name) =>
            new(name);
    }
}