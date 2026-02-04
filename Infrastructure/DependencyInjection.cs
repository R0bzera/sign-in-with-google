using Application.Interfaces;
using Infrastructure.OAuth;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IExternalIdentityRepository, ExternalIdentityRepository>();

        services.Configure<GoogleAuthSettings>(configuration.GetSection("Google"));
        services.AddScoped<GoogleAuthProvider>();
        services.AddScoped<IExternalAuthProviderFactory, ExternalAuthProviderFactory>();

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}
