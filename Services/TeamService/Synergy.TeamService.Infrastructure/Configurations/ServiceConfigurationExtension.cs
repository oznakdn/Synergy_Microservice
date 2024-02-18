using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Infrastructure.Repositories.Implementations;

namespace Synergy.TeamService.Infrastructure.Configurations;

public static class ServiceConfigurationExtension
{
    public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddRedisService(configuration);
        services.AddScoped<ITeamRepo, TeamRepo>();
        services.AddScoped<IMemberRepo, MemberRepo>();
        services.AddScoped<ITechnologyRepo, TechnologyRepo>();
        services.AddScoped<IContactRepo, ContactRepo>();
        services.AddScoped<ISkillRepo, SkillRepo>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    private static void AddRedisService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(opt => ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!));
    }
}
