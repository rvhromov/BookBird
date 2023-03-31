using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.Feedback;

namespace BookBird.Domain.Factories.Feedbacks
{
    public interface IFeedbackFactory
    {
        Feedback Create(FeedbackDescription description, FeedbackRating rating, Book book);
    }
}