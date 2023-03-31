using System;
using BookBird.Application.CommandHandlers.Books.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Books
{
    public sealed class UpdateBookValidator : AbstractValidator<UpdateBook>
    {
        public UpdateBookValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
            
            RuleFor(x => x.PublishYear)
                .NotEmpty()
                .LessThanOrEqualTo((ushort) DateTime.UtcNow.Year);

            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .GreaterThan(default(Guid));

            RuleFor(x => x.GenreIds)
                .NotEmpty();

            RuleForEach(x => x.GenreIds)
                .GreaterThan(default(Guid));
        }
    }
}