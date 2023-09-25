using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Authentication.Commons;

public record AuthenticationResult(
    User user,
    string Token);