using BookBird.Infrastructure.QueryHandlers.Authors.Queries;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Authors
{
    public sealed class GetAuthorsValidator : AbstractValidator<GetAuthors>
    {
        public GetAuthorsValidator()
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