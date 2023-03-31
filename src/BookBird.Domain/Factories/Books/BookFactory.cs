using System.Collections.Generic;
using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Book;

namespace BookBird.Domain.Factories.Books
{
    public sealed class BookFactory : IBookFactory
    {
        public Book Create(BookName name, BookPublishYear publishYear, Author author, List<Genre> genres)
            => new(name, publishYear, author, genres);
    }
}