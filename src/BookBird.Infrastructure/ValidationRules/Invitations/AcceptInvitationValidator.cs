using BookBird.Application.CommandHandlers.Invitations.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Invitations
{
    public sealed class AcceptInvitationValidator : AbstractValidator<AcceptInvitation>
    {
        public AcceptInvitationValidator()
        {
            RuleFor(x => x.InvitationId)
                .NotEmpty();
        }
    }
}