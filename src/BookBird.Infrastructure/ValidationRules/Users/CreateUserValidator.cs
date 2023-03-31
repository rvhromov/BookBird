using BookBird.Application.CommandHandlers.Users.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Users
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MaximumLength(100);
            
            RuleFor(x => x.Email)
                .NotNull()
                .MaximumLength(100);
        }
    }
}