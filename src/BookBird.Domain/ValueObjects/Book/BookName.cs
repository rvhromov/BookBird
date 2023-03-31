using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Book
{
    public sealed record BookName
    {
        private const int MaxLength = 150;
        
        public string Value { get; }

        public BookName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Book name cannot be empty.");

            if (value.Length > MaxLength)
                throw new ValidationException("Invalid book name length.");
            
            Value = value;
        }

        public static implicit operator string(BookName name) =>
            name.Value;

        public static implicit operator BookName(string name) =>
            new(name);
    }
}