using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Books;
using BookBird.Application.Helpers;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.Helpers;
using BookBird.Infrastructure.QueryHandlers.Books.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Books
{
    internal sealed class GetBooksHandler : IRequestHandler<GetBooks, IPaginatedList<BookWithAuthorDto>>
    {
        private readonly DbSet<BookReadModel> _books;
        private readonly IMapper _mapper;

        public GetBooksHandler(ReadDbContext context, IMapper mapper)
        {
            _books = context.Books;
            _mapper = mapper;
        }

        public async Task<IPaginatedList<BookWithAuthorDto>> Handle(GetBooks request, CancellationToken cancellationToken)
        {
            var (skip, take, name, authorName) = request;

            var booksQuery = _books
                .Include(b => b.Author)
                .AsNoTracking()
                .Where(b => b.Status == Status.Active);

            if (!string.IsNullOrWhiteSpace(name))
                booksQuery = booksQuery.Where(b => b.Name.Contains(name));

            if (!string.IsNullOrWhiteSpace(authorName))
            {
                var authorNameTokens = authorName.Split(' ');
                
                booksQuery = booksQuery.Where(b => 
                    authorNameTokens.Contains(b.Author.FirstName) || 
                    authorNameTokens.Contains(b.Author.LastName));
            }
            
            booksQuery = booksQuery.OrderBy(b => b.Name);

            var totalCount = await booksQuery.CountAsync(cancellationToken);

            var books = await booksQuery
                .Skip(skip)
                .Take(take)
                .ProjectTo<BookWithAuthorDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaginatedList<BookWithAuthorDto>(totalCount, books);
        }
    }
}