using BookBird.Application.CommandHandlers.Genres.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Genres
{
    public sealed class UpdateGenreValidator : AbstractValidator<UpdateGenre>
    {
        public UpdateGenreValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(1000);
        }
    }
}