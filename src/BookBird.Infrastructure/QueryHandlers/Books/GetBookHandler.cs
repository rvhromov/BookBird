using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.Books;
using BookBird.Domain.Enums;
using BookBird.Domain.Exceptions;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using BookBird.Infrastructure.QueryHandlers.Books.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.QueryHandlers.Books
{
    internal sealed class GetBookHandler : IRequestHandler<GetBook, BookExtendedDto>
    {
        private readonly DbSet<BookReadModel> _books;
        private readonly IMapper _mapper;

        public GetBookHandler(ReadDbContext context, IMapper mapper)
        {
            _books = context.Books;
            _mapper = mapper;
        }
        
        public async Task<BookExtendedDto> Handle(GetBook request, CancellationToken cancellationToken) => 
            await _books
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .AsNoTracking()
                .Where(b => b.Status == Status.Active)
                .ProjectTo<BookExtendedDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException("Book not found.");
    }
}