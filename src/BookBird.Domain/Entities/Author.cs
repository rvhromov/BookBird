using System;
using System.Collections.Generic;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Primitives;
using BookBird.Domain.ValueObjects.Author;

namespace BookBird.Domain.Entities
{
    public class Author : Entity
    {
        internal Author(AuthorFirstName firstName, AuthorLastName lastName) 
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private Author()
        {
        }

        public AuthorFirstName FirstName { get; private set; }
        public AuthorLastName LastName { get; private set; }

        #region Book

        private readonly List<Book> _books = new();
        public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

        internal void AddBook(Book book)
        {
            if (book is null)
                throw new ValidationException("Book cannot be null.");
            
            _books.Add(book);
        }
        
        internal void RemoveBook(Book book)
        {
            if (book is null)
                return;

            _books.Remove(book);
        }
        
        #endregion

        public void Update(AuthorFirstName firstName, AuthorLastName lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            SetModificationDate(DateTime.UtcNow);
        }
    }
}