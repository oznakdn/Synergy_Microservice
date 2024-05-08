using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synergy.ProjectService.Infrastructure.Configurations;
using Synergy.ProjectService.Infrastructure.Context;
using System.Reflection;

namespace Synergy.ProjectService.Appliocation.Configurations;

public static class ServiceConfigurationExtension
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureService(configuration);
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }

    public static void AddAutoMigration(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
}
