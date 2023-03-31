using BookBird.Domain.Primitives;

namespace BookBird.Domain.Exceptions
{
    public class NotFoundException : BookBirdException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}