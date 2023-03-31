using BookBird.Infrastructure.QueryHandlers.Genres.Queries;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Genres
{
    public sealed class GetGenresValidator : AbstractValidator<GetGenres>
    {
        public GetGenresValidator()
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