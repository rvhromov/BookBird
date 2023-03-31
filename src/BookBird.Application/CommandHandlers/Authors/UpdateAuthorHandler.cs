using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Authors.Commands;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Authors
{
    internal sealed class UpdateAuthorHandler : IRequestHandler<UpdateAuthor>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorHandler(IAuthorRepository authorRepository) => 
            _authorRepository = authorRepository;

        public async Task<Unit> Handle(UpdateAuthor request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAsync(request.Id)
                ?? throw new NotFoundException("Author not found.");
            
            author.Update(request.FirstName, request.LastName);
            await _authorRepository.UpdateAsync(author);
            
            return Unit.Value;
        }
    }
}