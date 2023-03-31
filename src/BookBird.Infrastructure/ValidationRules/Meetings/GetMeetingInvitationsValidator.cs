using BookBird.Infrastructure.QueryHandlers.Invitations.Queries;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Meetings
{
    public sealed class GetMeetingInvitationsValidator : AbstractValidator<GetMeetingInvitations>
    {
        public GetMeetingInvitationsValidator()
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