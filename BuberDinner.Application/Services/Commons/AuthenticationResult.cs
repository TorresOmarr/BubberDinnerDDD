using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Commons;

public record AuthenticationResult(
    User user,
    string Token);