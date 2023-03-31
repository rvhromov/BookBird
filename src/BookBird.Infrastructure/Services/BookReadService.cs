using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBird.Application.DTOs.BookSearch;
using BookBird.Application.Services;
using BookBird.Domain.Enums;
using BookBird.Infrastructure.EF.Contexts;
using BookBird.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBird.Infrastructure.Services
{
    internal sealed class BookReadService : IBookReadService
    {
        private readonly DbSet<BookReadModel> _books;
        private readonly IMapper _mapper;

        public BookReadService(ReadDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _books = context.Books;
        }

        public Task<bool> ExistAsync(string name, string authorFirstName, string authorLastName) =>
            _books.AnyAsync(b => 
                b.Name == name && 
                b.Author.FirstName == authorFirstName && 
                b.Author.LastName == authorLastName);

        public Task<List<BookIndexDto>> GetBooksForIndexingAsync(Status status, int takeForHours)
        {
            var requestedTime = DateTime.UtcNow.AddHours(-takeForHours);

            return _books
                .AsNoTracking()
                .Where(b => b.Status == status && b.ModifiedAt >= requestedTime)
                .ProjectTo<BookIndexDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}