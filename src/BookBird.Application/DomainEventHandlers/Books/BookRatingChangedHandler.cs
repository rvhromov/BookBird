using System.Threading;
using System.Threading.Tasks;
using BookBird.Domain.DomainEvents.Books;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.DomainEventHandlers.Books
{
    internal sealed class BookRatingChangedHandler : INotificationHandler<BookRatingChangedDomainEvent>
    {
        private readonly IBookRepository _bookRepository;

        public BookRatingChangedHandler(IBookRepository bookRepository) => 
            _bookRepository = bookRepository;

        public async Task Handle(BookRatingChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetAsync(notification.BookId) 
                ?? throw new NotFoundException("Book not found.");

            book.CalculateRating();
            await _bookRepository.UpdateAsync(book);
        }
    }
}