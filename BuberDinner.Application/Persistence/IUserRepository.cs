using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Persistence;


public interface IUserRepository{
    void Add(User user);
    User? GetUserByEmail(string email);
}