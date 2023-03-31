using System;
using BookBird.Application.CommandHandlers.Meetings.Commands;
using BookBird.Domain.Enumerations;
using FluentValidation;

namespace BookBird.Infrastructure.ValidationRules.Meetings
{
    public sealed class CreateMeetingValidator : AbstractValidator<CreateMeeting>
    {
        public CreateMeetingValidator()
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

            RuleFor(x => x.Type)
                .NotEmpty();

            RuleFor(x => x)
                .Custom((model, context) =>
                {
                    if (Equals(model.Type, MeetingType.Limited) && model.MaxNumberOfVisitors is null)
                    {
                        context.AddFailure(nameof(CreateMeeting.MaxNumberOfVisitors), "Maximum number of visitors required when meeting type is Limited");
                    }
                });

            RuleFor(x => x.OwnerId)
                .NotEmpty();
        }
    }
}