using BookBird.Domain.Entities;
using BookBird.Domain.ValueObjects.User;

namespace BookBird.Domain.Factories.Users
{
    public class UserFactory : IUserFactory
    {
        public User Create(UserName name, UserEmail email) => 
            new(name, email);
    }
}