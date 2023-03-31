using BookBird.Application.CommandHandlers.Invitations.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Invitations
{
    public sealed class CreateInvitationValidator : AbstractValidator<CreateInvitation>
    {
        public CreateInvitationValidator()
        {
            RuleFor(x => x.MeetingId)
                .NotEmpty();
            
            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}