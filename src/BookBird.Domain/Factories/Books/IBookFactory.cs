using System.Collections.Generic;
using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Book;

namespace BookBird.Domain.Factories.Books
{
    public interface IBookFactory
    {
        Book Create(BookName name, BookPublishYear publishYear, Author author, List<Genre> genres);
    }
}