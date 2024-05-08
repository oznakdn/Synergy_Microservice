using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synergy.TeamService.Infrastructure.Configurations;
using System.Reflection;
using FluentValidation;
using Synergy.TeamService.Infrastructure.Context;

namespace Synergy.TeamService.Application.Configurations;

public static class ServiceConfigurationExtension
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureService(configuration);
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public static void AddAutoMigration(this IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
}
