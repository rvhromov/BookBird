using BookBird.Domain.Primitives;

namespace BookBird.Domain.Exceptions
{
    public class ValidationException : BookBirdException
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}