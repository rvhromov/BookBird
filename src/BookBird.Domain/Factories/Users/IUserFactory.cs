using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.User;

namespace BookBird.Domain.Factories.Users
{
    public interface IUserFactory
    {
        User Create(UserName name, UserEmail email);
    }
}