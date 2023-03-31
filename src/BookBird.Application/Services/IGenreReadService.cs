using System.Threading.Tasks;

namespace BookBird.Application.Services
{
    public interface IGenreReadService
    {
        Task<bool> ExistAsync(string name);
    }
}