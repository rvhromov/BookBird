using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Authors;
using BookBird.Application.Helpers;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.Helpers;
using BookBird.Infrastructure.QueryHandlers.Authors.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Authors
{
    internal sealed class GetAuthorsHandler : IRequestHandler<GetAuthors, IPaginatedList<AuthorDto>>
    {
        private readonly DbSet<AuthorReadModel> _authors;
        private readonly IMapper _mapper;

        public GetAuthorsHandler(ReadDbContext context, IMapper mapper)
        {
            _authors = context.Authors;
            _mapper = mapper;
        }

        public async Task<IPaginatedList<AuthorDto>> Handle(GetAuthors request, CancellationToken cancellationToken)
        {
            var (skip, take, name) = request;

            var authorsQuery = _authors
                .AsNoTracking()
                .Where(a => a.Status == Status.Active);
            
            if (!string.IsNullOrWhiteSpace(name))
                authorsQuery = authorsQuery.Where(a => a.FirstName.Contains(name) || a.LastName.Contains(name));

            authorsQuery = authorsQuery.OrderBy(a => a.LastName);

            var totalCount = await authorsQuery.CountAsync(cancellationToken);

            var authors = await authorsQuery
                .Skip(skip)
                .Take(take)
                .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedList<AuthorDto>(totalCount, authors);
        }
    }
}