using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Author
{
    public sealed record AuthorLastName
    {
        private const int MaxLength = 50;
        
        public string Value { get; }
        
        public AuthorLastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Author last name cannot be empty.");

            if (value.Length > MaxLength)
                throw new ValidationException("Invalid author last name length.");
            
            Value = value;
        }
        
        public static implicit operator string(AuthorLastName firstName) =>
            firstName.Value;

        public static implicit operator AuthorLastName(string name) =>
            new(name);
    }
}