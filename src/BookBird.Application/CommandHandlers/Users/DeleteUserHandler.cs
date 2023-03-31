using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Users.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Users
{
    internal sealed class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository) => 
            _userRepository = userRepository;
        
        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id) 
                ?? throw new NotFoundException("User not found.");

            user.Archive();
            await _userRepository.UpdateAsync(user);
            
            return Unit.Value;
        }
    }
}