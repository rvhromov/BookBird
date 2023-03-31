using System.Net.Mail;
using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.User
{
    public sealed record UserEmail
    {
        private const int MaxLength = 100;
        
        public string Value { get; }

        public UserEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("User email cannot be empty.");
            
            if (value.Length > MaxLength)
                throw new ValidationException("Invalid user email length.");
            
            if (!MailAddress.TryCreate(value, out var mailAddress))
                throw new ValidationException("Invalid user email format.");
            
            Value = value;
        }

        public static implicit operator string(UserEmail email) =>
            email.Value;
        
        public static implicit operator UserEmail(string email) =>
            new(email);
    }
}