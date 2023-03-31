using BookBird.Application.CommandHandlers.Feedbacks.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Feedbacks
{
    public sealed class CreateFeedbackValidator : AbstractValidator<CreateFeedback>
    {
        public CreateFeedbackValidator()
        {
            RuleFor(x => x.BookId)
                .NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.Rating)
                .NotEmpty()
                .InclusiveBetween((ushort) 1, (ushort) 5);
        }
    }
}