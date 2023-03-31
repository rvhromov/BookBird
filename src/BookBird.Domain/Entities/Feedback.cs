using System;
using BookBird.Domain.DomainEvents;
using BookBird.Domain.DomainEvents.Books;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Primitives;
using BookBird.Domain.ValueObjects.Feedback;

namespace BookBird.Domain.Entities
{
    public class Feedback : Entity
    {
        internal Feedback(FeedbackDescription description, FeedbackRating rating, Book book)
        {
            Description = description;
            Rating = rating;
            AddBook(book);
        }
        
        private Feedback()
        {
        }
        
        public FeedbackDescription Description { get; private set; }
        public FeedbackRating Rating { get; private set; }

        public Guid BookId { get; private set; }
        public Book Book { get; private set; }

        private void AddBook(Book book)
        {
            if (book is null)
                throw new ValidationException("Book cannot be null.");

            book.AddFeedback(this);
            Book = book;
        }

        public void Update(FeedbackDescription description, FeedbackRating rating)
        {
            Description = description;
            Rating = rating;
            SetModificationDate(DateTime.UtcNow);
            
            AddEvent(new BookRatingChangedDomainEvent(Book.Id));
        }

        public void Archive()
        {
            Status = Status.Deleted;
            SetModificationDate(DateTime.UtcNow);
            
            AddEvent(new BookRatingChangedDomainEvent(Book.Id));
        }
    }
}