using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.RoleCommands.CreateRole;

/// Handles creating a new role in the system by validating the role name is unique and persisting the new role entity.
/// Returns a result indicating success or failure.
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result>
{
    private readonly IRoleRepository roleRepo;

    public CreateRoleCommandHandler(IRoleRepository roleRepo)
    {
        this.roleRepo = roleRepo;
    }

    public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var existRole = await roleRepo.GetAsync(_ => _.RoleName.ToLower() == request.CreateRole.RoleName.ToLower());

        if (existRole is not null)
            return Result.Failure(400,error: "Role is already exist!");

        await roleRepo.CreateAsync(new Domain.Models.Role
        {
            RoleName = request.CreateRole.RoleName,
            Description = request.CreateRole.Description
        });

        return Result.Success(200);
    }
}

/*
The CreateRoleCommandHandler class handles creating a new role in the system.

It takes a CreateRoleCommand object as input, which contains the details of the role to create such as the role name and description.

It returns a Result object indicating whether the role creation succeeded or failed.

The main logic is in the Handle method. It first checks if a role with the given name already exists by calling the roleRepo to query the database. If a role with that name exists, it returns a failure Result.

Otherwise, it creates a new Role domain object with the name and description from the command. It saves this new role to the database by calling the roleRepo's CreateAsync method.

Finally, if no errors occurred, it returns a successful Result.

The key steps are:

1. Validate role name is unique
2. Create Role domain object
3. Persist new role entity using repository
4. Return result indicating success/failure
5. This allows the application to attempt to create a new role, while encapsulating the database persistence and validation logic within this handler class. The use of the Result object abstracts away the details and provides a simple success/fail response.

*/
