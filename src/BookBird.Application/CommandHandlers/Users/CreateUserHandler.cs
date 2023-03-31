using System;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Users.Commands;
using BookBird.Application.IntegrationEvents.Users;
using BookBird.Application.Services;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Users;
using BookBird.Domain.Repositories;
using MassTransit;
using MediatR;

namespace BookBird.Application.CommandHandlers.Users
{
    internal sealed class CreateUserHandler : IRequestHandler<CreateUser, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly IUserReadService _userReadService;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateUserHandler(
            IUserRepository userRepository, 
            IUserFactory userFactory, 
            IUserReadService userReadService, 
            IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
            _userReadService = userReadService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Guid> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var (name, email) = request;

            if (await _userReadService.ExistAsync(email))
            {
                throw new ValidationException("User with the same email already exists.");
            }

            var user = _userFactory.Create(name, email);
            var id = await _userRepository.AddAsync(user);
            
            await _publishEndpoint.Publish(new UserCreatedIntegrationEvent(id), cancellationToken);

            return id;
        }
    }
}