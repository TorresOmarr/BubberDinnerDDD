using BuberDinner.Domain.User;

namespace BuberDinner.Application.Authentication.Commons;

public record AuthenticationResult(
    User User,
    string Token);