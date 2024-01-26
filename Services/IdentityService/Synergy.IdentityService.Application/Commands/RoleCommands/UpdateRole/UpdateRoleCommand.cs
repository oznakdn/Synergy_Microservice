using MediatR;
using Synergy.IdentityService.Shared.Dtos.RoleDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.RoleCommands.UpdateRole;

public class UpdateRoleCommand : IRequest<Result>
{
    public UpdateRoleCommand(UpdateRoleDto updateRole)
    {
        UpdateRole = updateRole;
    }

    public UpdateRoleDto UpdateRole { get; set; }
}