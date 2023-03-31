using System;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Feedbacks.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Feedbacks;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Feedbacks
{
    internal sealed class CreateFeedbackHandler : IRequestHandler<CreateFeedback, Guid>
    {
        private readonly IFeedbackFactory _feedbackFactory;
        private readonly IFeedbackRepository _feedbackRepository; 
        private readonly IBookRepository _bookRepository;

        public CreateFeedbackHandler(
            IFeedbackFactory feedbackFactory, 
            IFeedbackRepository feedbackRepository, 
            IBookRepository bookRepository)
        {
            _feedbackFactory = feedbackFactory;
            _feedbackRepository = feedbackRepository;
            _bookRepository = bookRepository;
        }

        public async Task<Guid> Handle(CreateFeedback request, CancellationToken cancellationToken)
        {
            var (description, rating, bookId) = request;
            
            var book = await _bookRepository.GetAsync(bookId)
                ?? throw new NotFoundException("Book not found.");
            
            var feedback = _feedbackFactory.Create(description, rating, book);
            var id = await _feedbackRepository.AddAsync(feedback);
            
            return id;
        }
    }
}