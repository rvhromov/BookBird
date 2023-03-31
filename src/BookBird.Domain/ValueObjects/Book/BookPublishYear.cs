using System;
using BookBird.Domain.Exceptions;

namespace BookBird.Domain.ValueObjects.Book
{
    public sealed record BookPublishYear
    {
        public ushort Value { get; }
        
        public BookPublishYear(ushort value)
        {
            if (value == default || value > DateTime.UtcNow.Year)
                throw new ValidationException("Invalid book publish year.");

            Value = value;
        }

        public static implicit operator ushort(BookPublishYear publishYear) =>
            publishYear.Value;

        public static implicit operator BookPublishYear(ushort publishYear) =>
            new(publishYear);
    }
}