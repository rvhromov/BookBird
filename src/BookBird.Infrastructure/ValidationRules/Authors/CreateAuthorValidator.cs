using BookBird.Application.CommandHandlers.Authors.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Authors
{
    public sealed class CreateAuthorValidator : AbstractValidator<CreateAuthor>
    {
        public CreateAuthorValidator()
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