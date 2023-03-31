using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Authors;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.QueryHandlers.Authors.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Authors
{
    internal sealed class GetAuthorHandler : IRequestHandler<GetAuthor, AuthorWithBooksDto>
    {
        private readonly DbSet<AuthorReadModel> _authors;
        private readonly IMapper _mapper;

        public GetAuthorHandler(ReadDbContext context, IMapper mapper)
        {
            _authors = context.Authors;
            _mapper = mapper;
        }
        
        public async Task<AuthorWithBooksDto> Handle(GetAuthor request, CancellationToken cancellationToken) => 
            await _authors
                .Include(a => a.Books)
                .AsNoTracking()
                .Where(a => a.Status == Status.Active)
                .ProjectTo<AuthorWithBooksDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException("Author not found.");
    }
}