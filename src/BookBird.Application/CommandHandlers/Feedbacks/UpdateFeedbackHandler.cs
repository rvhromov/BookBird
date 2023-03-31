using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Feedbacks.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Feedbacks
{
    internal sealed class UpdateFeedbackHandler : IRequestHandler<UpdateFeedback>
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public UpdateFeedbackHandler(IFeedbackRepository feedbackRepository) => 
            _feedbackRepository = feedbackRepository;

        public async Task<Unit> Handle(UpdateFeedback request, CancellationToken cancellationToken)
        {
            var feedback = await _feedbackRepository.GetAsync(request.Id) 
                ?? throw new NotFoundException("Feedback not found.");
            
            feedback.Update(request.Description, request.Rating);
            await _feedbackRepository.UpdateAsync(feedback);
            
            return Unit.Value;
        }
    }
}