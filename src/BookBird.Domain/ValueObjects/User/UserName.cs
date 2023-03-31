using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.User
{
    public sealed record UserName
    {
        private const int MaxLength = 100;
        
        public string Value { get; }

        public UserName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("User name cannot be empty.");
            
            if (value.Length > MaxLength)
                throw new ValidationException("Invalid user name length.");
            
            Value = value;
        }

        public static implicit operator string(UserName name) =>
            name.Value;
        
        public static implicit operator UserName(string name) =>
            new(name);
    }
}