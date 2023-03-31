using System.Threading.Tasks;

namespace BookBird.Application.Services
{
    public interface IAuthorReadService
    {
        Task<bool> ExistAsync(string firstName, string lastName);
    }
}