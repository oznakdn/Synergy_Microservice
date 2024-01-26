using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Infrastructure.Repositories.Implementations;

namespace Synergy.TeamService.Infrastructure.Configurations;

public static class ServiceConfigurationExtension
{
    public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<ITeamRepo, TeamRepo>();
        services.AddScoped<IDeveloperRepo,DeveloperRepo>();
        services.AddScoped<ITechnologyRepo,TechnologyRepo>();
        services.AddScoped<IContactRepo,ContactRepo>();
        services.AddScoped<IDeveloperSkillRepo, DeveloperSkillRepo>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}
