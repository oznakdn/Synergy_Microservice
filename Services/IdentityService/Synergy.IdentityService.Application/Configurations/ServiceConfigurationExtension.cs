using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Synergy.IdentityService.Application.TokenService;
using Synergy.IdentityService.Application.TokenService.Options;
using Synergy.IdentityService.Infrastructure.Options;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Infrastructure.Repositories.Implementations;
using System.Reflection;

namespace Synergy.IdentityService.Application.Configurations;

public static class ServiceConfigurationExtension
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoOption>(configuration.GetSection(nameof(MongoOption)));
        services.AddSingleton<IMongoOption>(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value);
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.Configure<TokenOption>(configuration.GetSection(nameof(TokenOption)));
        services.AddSingleton<ITokenOption>(sp => sp.GetRequiredService<IOptions<TokenOption>>().Value);
        services.AddScoped<ITokenGenerator, TokenGenerator>();
    }
}
