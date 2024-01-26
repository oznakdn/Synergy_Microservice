using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synergy.TeamService.Infrastructure.Configurations;
using System.Reflection;
using FluentValidation;

namespace Synergy.TeamService.Application.Configurations;

public static class ServiceConfigurationExtension
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureService(configuration);
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
