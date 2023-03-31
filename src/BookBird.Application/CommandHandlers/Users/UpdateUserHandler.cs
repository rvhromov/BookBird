using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Users.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Users
{
    internal sealed class UpdateUserHandler : IRequestHandler<UpdateUser>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository) => 
            _userRepository = userRepository;
        
        public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.Id) 
                ?? throw new NotFoundException("User not found.");
            
            user.Update(request.Name, request.Email);
            await _userRepository.UpdateAsync(user);
            
            return Unit.Value;
        }
    }
}