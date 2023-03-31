using System;
using System.Collections.Generic;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Primitives;
using BookBird.Domain.ValueObjects.Genre;

namespace BookBird.Domain.Entities
{
    public class Genre : Entity
    {
        internal Genre(GenreName name, GenreDescription description)
        {
            Name = name;
            Description = description;
        }
        
        private Genre()
        {
        }
        
        public GenreName Name { get; private set; }
        public GenreDescription Description { get; private set; }

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

        public void Update(GenreName name, GenreDescription description)
        {
            Name = name;
            Description = description;
            SetModificationDate(DateTime.UtcNow);
        }
        
        public void Archive()
        {
            Status = Status.Deleted;
            SetModificationDate(DateTime.UtcNow);
        }
    }
}