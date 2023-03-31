using System.Collections.Generic;
using System.Threading.Tasks;
using BookBird.Application.DTOs.BookSearch;
using BookBird.Domain.Enums;

namespace BookBird.Application.Services
{
    public interface IBookReadService
    {
        Task<bool> ExistAsync(string name, string authorFirstName, string authorLastName);
        Task<List<BookIndexDto>> GetBooksForIndexingAsync(Status status, int takeForHours);
    }
}