using MediatR;
using Synergy.IdentityService.Shared.Dtos.RoleDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.RoleCommands.CreateRole;

public class CreateRoleCommand : IRequest<Result>
{
    public CreateRoleDto CreateRole { get; set; }
}
