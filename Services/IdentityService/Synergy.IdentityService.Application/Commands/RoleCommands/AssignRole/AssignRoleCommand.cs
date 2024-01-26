using MediatR;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.RoleCommands.AssignRole;

public class AssignRoleCommand : IRequest<Result>
{
    public string UserId { get; set; }
    public string RoleId { get; set; }
}
