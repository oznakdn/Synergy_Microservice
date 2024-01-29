using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synergy.ProjectService.Infrastructure.Configurations;
using System.Reflection;

namespace Synergy.ProjectService.Appliocation.Configurations;

public static class ServiceConfigurationExtension
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureService(configuration);
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
