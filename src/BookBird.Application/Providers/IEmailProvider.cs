using System.Threading.Tasks;
using BookBird.Application.DTOs.Emails;

namespace BookBird.Application.Providers
{
    public interface IEmailProvider
    {
        Task SendAsync(EmailDto email);
    }
}