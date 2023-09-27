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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;

namespace BuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager conf)
    {
        services.AddAuth(conf);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IUserRepository, UserRepository>();
        return services;
    }
    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager conf)
    {
        var JwtSettings = new JwtSettings();
        conf.Bind(JwtSettings.SectionName, JwtSettings);
        services.AddSingleton(Options.Create(JwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = JwtSettings.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(JwtSettings.Secret)),
                    ValidateLifetime = true,
                };
            });
        return services;
    }
}