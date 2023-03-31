using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Author;

namespace BookBird.Domain.Factories.Authors
{
    public interface IAuthorFactory
    {
        Author Create(AuthorFirstName firstName, AuthorLastName lastName);
    }
}