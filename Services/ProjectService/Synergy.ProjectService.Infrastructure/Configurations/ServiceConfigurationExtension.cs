using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synergy.ProjectService.Infrastructure.Context;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;
using Synergy.ProjectService.Infrastructure.Repositories.Implementations;

namespace Synergy.ProjectService.Infrastructure.Configurations;

public static class ServiceConfigurationExtension
{
    public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IProjectRepo, ProjectRepo>();
        services.AddScoped<ICaseRepo, CaseRepo>();
        services.AddScoped<IRepositoryManager,RepositoryManager > ();

    }
}
