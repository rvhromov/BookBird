using System;
using System.Threading;
using System.Threading.Tasks;
using BookBird.Application.CommandHandlers.Authors.Commands;
using BookBird.Application.Services;
using BookBird.Domain.Exceptions;
using BookBird.Domain.Factories.Authors;
using BookBird.Domain.Repositories;
using MediatR;

namespace BookBird.Application.CommandHandlers.Authors
{
    internal sealed class CreateAuthorHandler : IRequestHandler<CreateAuthor, Guid>
    {
        private readonly IAuthorFactory _authorFactory;
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorReadService _authorReadService;

        public CreateAuthorHandler(
            IAuthorFactory authorFactory, 
            IAuthorRepository authorRepository, 
            IAuthorReadService authorReadService)
        {
            _authorFactory = authorFactory;
            _authorRepository = authorRepository;
            _authorReadService = authorReadService;
        }

        public async Task<Guid> Handle(CreateAuthor request, CancellationToken cancellationToken)
        {
            var (firstName, lastName) = request;

            if (await _authorReadService.ExistAsync(firstName, lastName))
                throw new ValidationException($"Author {firstName} {lastName} already exists.");

            var author = _authorFactory.Create(firstName, lastName);

            return await _authorRepository.AddAsync(author);
        }
    }
}