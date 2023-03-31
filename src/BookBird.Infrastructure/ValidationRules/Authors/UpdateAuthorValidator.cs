using BookBird.Application.CommandHandlers.Authors.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Authors
{
    public sealed class UpdateAuthorValidator : AbstractValidator<UpdateAuthor>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);
            
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}