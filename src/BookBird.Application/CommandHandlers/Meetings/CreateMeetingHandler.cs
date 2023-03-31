using System;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Meetings.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Meetings;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Meetings
{
    internal sealed class CreateMeetingHandler : IRequestHandler<CreateMeeting, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMeetingFactory _meetingFactory;
        private readonly IMeetingRepository _meetingRepository;

        public CreateMeetingHandler(
            IUserRepository userRepository, 
            IMeetingFactory meetingFactory, 
            IMeetingRepository meetingRepository)
        {
            _userRepository = userRepository;
            _meetingFactory = meetingFactory;
            _meetingRepository = meetingRepository;
        }

        public async Task<Guid> Handle(CreateMeeting request, CancellationToken cancellationToken)
        {
            var (name, location, scheduledFor, type, numberOfVisitors, ownerId) = request;
            
            var owner = await _userRepository.GetAsync(ownerId) 
                ?? throw new NotFoundException("User not found");
            
            var meeting = _meetingFactory.Create(name, location, scheduledFor, type, numberOfVisitors, owner);
            return await _meetingRepository.AddAsync(meeting);
        }
    }
}