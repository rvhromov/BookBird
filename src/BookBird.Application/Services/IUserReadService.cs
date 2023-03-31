using System.Threading.Tasks;

namespace BookBird.Application.Services
{
    public interface IUserReadService
    {
        Task<bool> ExistAsync(string email);
    }
}