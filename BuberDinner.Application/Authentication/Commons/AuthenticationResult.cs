using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Authentication.Commons;

public record AuthenticationResult(
    User User,
    string Token);