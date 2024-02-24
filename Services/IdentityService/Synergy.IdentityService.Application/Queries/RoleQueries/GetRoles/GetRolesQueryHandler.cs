using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.RoleDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.RoleQueries.GetRoles;

/// Gets all roles from the role repository, maps them to RoleDto objects, 
/// and returns a Result with the RoleDto list on success.
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

/*
This class handles the query to get all roles. Its purpose is to retrieve the list of roles from the database, convert them to RoleDto objects, and return the list of RoleDto.

It takes a GetRolesQuery as input, which represents the request to get the list of roles.

It returns a Result as output. This Result will contain a list of RoleDto objects if successful.

To achieve this, it depends on the IRoleRepository to retrieve the list of roles from the database. In the constructor, it gets an instance of IRoleRepository injected.

In the Handle method, it calls roleRepo.GetAllAsync to get the list of roles from the database.

It then loops through the list of roles, converts each one to a RoleDto object by mapping the Id, RoleName and Description properties.

The RoleDto objects are added to a List which is then wrapped in a Result.Success object and returned.

So in summary, it retrieves a list of roles from the database, converts them to DTOs, and returns the list of DTOs in a Result object. The main logic is querying the database, mapping to DTOs, and wrapping in a result.
*/