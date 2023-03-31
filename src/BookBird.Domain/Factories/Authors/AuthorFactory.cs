using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Author;

namespace BookBird.Domain.Factories.Authors
{
    public sealed class AuthorFactory : IAuthorFactory
    {
        public Author Create(AuthorFirstName firstName, AuthorLastName lastName) =>
            new(firstName, lastName);
    }
}