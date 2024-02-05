using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.RoleDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.RoleQueries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Result<RoleDto>>
{
    private readonly IRoleRepository roleRepo;

    public GetRolesQueryHandler(IRoleRepository roleRepo)
    {
        this.roleRepo = roleRepo;
    }

    public async Task<Result<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleRepo.GetAllAsync(cancellationToken: cancellationToken);

        List<RoleDto> roleDto = roles.Select(_ => new RoleDto(_.Id, _.RoleName, _.Description)).ToList();

        return Result<RoleDto>.Success(statusCode: 200, values: roleDto);

    }
}
