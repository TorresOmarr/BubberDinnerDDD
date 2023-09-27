using System.Runtime.Serialization;
using System.Net.Security;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Services;
using BuberDinner.Application.Persistence;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration conf )
    {
        services.Configure<JwtSettings>(conf.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();        
        services.AddSingleton<IUserRepository, UserRepository>();
        return services;
    }
}