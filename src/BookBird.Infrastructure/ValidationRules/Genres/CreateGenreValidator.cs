using BookBird.Application.CommandHandlers.Genres.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Genres
{
    public sealed class CreateGenreValidator : AbstractValidator<CreateGenre>
    {
        public CreateGenreValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}