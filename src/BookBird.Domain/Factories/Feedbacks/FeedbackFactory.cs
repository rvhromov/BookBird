using BookBird.Domain.DomainEvents;
using BookBird.Domain.DomainEvents.Books;
using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Feedback;

namespace BookBird.Domain.Factories.Feedbacks
{
    public class FeedbackFactory : IFeedbackFactory
    {
        public Feedback Create(FeedbackDescription description, FeedbackRating rating, Book book)
        {
            var feedback = new Feedback(description, rating, book);
            
            feedback.AddEvent(new BookRatingChangedDomainEvent(book.Id));

            return feedback;
        }
    }
}