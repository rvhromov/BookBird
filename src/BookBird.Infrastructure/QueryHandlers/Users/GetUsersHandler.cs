using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Users;
using BookBird.Application.Helpers;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.Helpers;
using BookBird.Infrastructure.QueryHandlers.Users.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Users
{
    internal sealed class GetUsersHandler : IRequestHandler<GetUsers, IPaginatedList<UserDto>>
    {
        private readonly DbSet<UserReadModel> _users;
        private readonly IMapper _mapper;

        public GetUsersHandler(ReadDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _users = context.Users;
        }

        public async Task<IPaginatedList<UserDto>> Handle(GetUsers request, CancellationToken cancellationToken)
        {
            var (skip, take, name) = request;

            var usersQuery = _users
                .AsNoTracking()
                .Where(u => u.Status == Status.Active);

            if (!string.IsNullOrWhiteSpace(name))
                usersQuery = usersQuery.Where(u => u.Name.StartsWith(name));

            usersQuery = usersQuery.OrderBy(u => u.Name);

            var totalCount = await usersQuery.CountAsync(cancellationToken);

            var users = await usersQuery
                .Skip(skip)
                .Take(take)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedList<UserDto>(totalCount, users);
        }
    }
}