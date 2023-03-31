using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Genres;
using BookBird.Application.Helpers;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.Helpers;
using BookBird.Infrastructure.QueryHandlers.Genres.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Genres
{
    internal sealed class GetGenresHandler : IRequestHandler<GetGenres, IPaginatedList<GenreDto>>
    {
        private readonly DbSet<GenreReadModel> _genres;
        private readonly IMapper _mapper;

        public GetGenresHandler(ReadDbContext context, IMapper mapper)
        {
            _genres = context.Genres;
            _mapper = mapper;
        }

        public async Task<IPaginatedList<GenreDto>> Handle(GetGenres request, CancellationToken cancellationToken)
        {
            var (skip, take, name) = request;
            
            var genresQuery = _genres
                .AsNoTracking()
                .Where(g => g.Status == Status.Active);

            if (!string.IsNullOrWhiteSpace(name))
                genresQuery = genresQuery.Where(g => g.Name.Contains(name));

            genresQuery = genresQuery.OrderBy(g => g.Name);

            var totalCount = await genresQuery.CountAsync(cancellationToken);

            var genres = await genresQuery
                .Skip(skip)
                .Take(take)
                .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedList<GenreDto>(totalCount, genres);
        }
    }
}