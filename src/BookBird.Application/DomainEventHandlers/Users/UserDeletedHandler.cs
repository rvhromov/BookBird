using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Domain.DomainEvents.Users;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.DomainEventHandlers.Users
{
    internal sealed class UserDeletedHandler : INotificationHandler<UserDeletedDomainEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMeetingVisitorRepository _meetingVisitorRepository;

        public UserDeletedHandler(
            IUserRepository userRepository, 
            IMeetingVisitorRepository meetingVisitorRepository)
        {
            _userRepository = userRepository;
            _meetingVisitorRepository = meetingVisitorRepository;
        }

        public async Task Handle(UserDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(notification.UserId) 
                ?? throw new NotFoundException("User not found.");

            var visitors = await _meetingVisitorRepository.GetVisitorsRelatedToUserAsync(user.Id);

            if (visitors is null || !visitors.Any())
                return;

            foreach (var visitor in visitors)
                visitor.Archive();

            await _meetingVisitorRepository.UpdateManyAsync(visitors);
        }
    }
}