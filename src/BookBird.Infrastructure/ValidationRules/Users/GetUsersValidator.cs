using BookBird.Infrastructure.QueryHandlers.Users.Queries;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Users
{
    public sealed class GetUsersValidator : AbstractValidator<GetUsers>
    {
        public GetUsersValidator()
        {
            RuleFor(x => x.Skip)
                .NotNull()
                .GreaterThanOrEqualTo(default(int));

            RuleFor(x => x.Take)
                .NotEmpty()
                .GreaterThan(default(int));

            RuleFor(x => x.Name)
                .MaximumLength(50);
        }
    }
}