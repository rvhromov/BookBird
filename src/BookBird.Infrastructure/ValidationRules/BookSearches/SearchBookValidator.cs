using BookBird.Infrastructure.QueryHandlers.BookSearch.Queries;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.BookSearches
{
    public sealed class SearchBookValidator : AbstractValidator<SearchBook>
    {
        public SearchBookValidator()
        {
            RuleFor(x => x.Skip)
                .NotNull()
                .GreaterThanOrEqualTo(default(int));

            RuleFor(x => x.Take)
                .NotEmpty()
                .GreaterThan(default(int));

            RuleFor(x => x.SearchTerm)
                .NotEmpty()
                .Length(min: 3, max: 100);
        }
    }
}