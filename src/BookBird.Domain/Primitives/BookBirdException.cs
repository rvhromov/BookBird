using System;

namespace BookBird.Domain.Primitives
{
    public abstract class BookBirdException : Exception
    {
        protected BookBirdException(string message) : base(message)
        {
        }
    }
}