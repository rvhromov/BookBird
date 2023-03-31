using BookBird.Infrastructure.QueryHandlers.MeetingVisitors.Queries;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Meetings
{
    public sealed class GetMeetingVisitorsValidator : AbstractValidator<GetMeetingVisitors>
    {
        public GetMeetingVisitorsValidator()
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