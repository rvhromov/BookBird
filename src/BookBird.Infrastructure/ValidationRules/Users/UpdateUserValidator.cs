using BookBird.Application.CommandHandlers.Users.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Users
{
    public sealed class UpdateUserValidator : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidator()
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