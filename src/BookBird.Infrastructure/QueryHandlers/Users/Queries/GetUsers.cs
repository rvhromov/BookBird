using BookBird.Application.DTOs.Users;
using BookBird.Application.Helpers;
using MediatR;

namespace BookBird.Infrastructure.QueryHandlers.Users.Queries
{
    public sealed record GetUsers(int Skip, int Take, string Name) : IRequest<IPaginatedList<UserDto>>;
}