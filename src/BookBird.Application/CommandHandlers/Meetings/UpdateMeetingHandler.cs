using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Meetings.Commands;
using BookBird.Application.IntegrationEvents.Meetings;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MassTransit;
using MediatR;

namespace BookBird.Application.CommandHandlers.Meetings
{
    internal sealed class UpdateMeetingHandler : IRequestHandler<UpdateMeeting>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateMeetingHandler(IMeetingRepository meetingRepository, IPublishEndpoint publishEndpoint)
        {
            _meetingRepository = meetingRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(UpdateMeeting request, CancellationToken cancellationToken)
        {
            var (name, location, scheduledFor, numberOfVisitors) = request;
            
            var meeting = await _meetingRepository.GetAsync(request.Id) 
                ?? throw new NotFoundException("Meeting not found.");
            
            meeting.Update(name, location, scheduledFor, numberOfVisitors);
            await _meetingRepository.UpdateAsync(meeting);
            
            await _publishEndpoint.Publish(new MeetingUpdatedIntegrationEvent(meeting.Id), cancellationToken);
            
            return Unit.Value;
        }
    }
}