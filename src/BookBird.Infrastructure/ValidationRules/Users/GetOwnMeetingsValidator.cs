using BookBird.Infrastructure.QueryHandlers.Meetings.Queries;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Users
{
    public sealed class GetOwnMeetingsValidator : AbstractValidator<GetOwnMeetings>
    {
        public GetOwnMeetingsValidator()
        {
            RuleFor(x => x.Skip)
                .NotNull()
                .GreaterThanOrEqualTo(default(int));

            RuleFor(x => x.Take)
                .NotEmpty()
                .GreaterThan(default(int));
        }
    }
}