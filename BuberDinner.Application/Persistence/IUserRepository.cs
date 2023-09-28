

using BuberDinner.Domain.User;

namespace BuberDinner.Application.Persistence;


public interface IUserRepository
{
    void Add(User user);
    User? GetUserByEmail(string email);
}