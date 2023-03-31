using BookBird.Infrastructure.QueryHandlers.Books.Queries;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Books
{
    public sealed class GetBooksValidator : AbstractValidator<GetBooks>
    {
        public GetBooksValidator()
        {
            RuleFor(x => x.Skip)
                .NotNull()
                .GreaterThanOrEqualTo(default(int));

            RuleFor(x => x.Take)
                .NotEmpty()
                .GreaterThan(default(int));

            RuleFor(x => x.Name)
                .MaximumLength(50);
            
            RuleFor(x => x.AuthorName)
                .MaximumLength(50);
        }
    }
}