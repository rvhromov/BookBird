using System;
using BookBird.Application.CommandHandlers.Meetings.Commands;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Meetings
{
    public sealed class UpdateMeetingValidator : AbstractValidator<UpdateMeeting>
    {
        public UpdateMeetingValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Location)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.ScheduledFor)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if (value < DateTime.UtcNow)
                    {
                        context.AddFailure(nameof(CreateMeeting.ScheduledFor), "Invalid meeting date. The date must be in a future.");
                    }
                });
        }
    }
}