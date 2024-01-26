using MediatR;
using Synergy.IdentityService.Shared.Dtos.RoleDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.RoleQueries.GetRoles;

public class GetRolesQuery : IRequest<Result<RoleDto>>
{
}
