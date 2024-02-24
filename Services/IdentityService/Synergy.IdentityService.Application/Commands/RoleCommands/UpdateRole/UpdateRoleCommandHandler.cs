using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.RoleCommands.UpdateRole;

/// Handles the UpdateRoleCommand request to update an existing role in the system.
/// Retrieves the existing role entity by ID from the role repository. 
/// Updates the role name, description, and other properties from the request model.
/// Calls the role repository to persist the updated entity.
/// Returns a successful 204 result if updated, else returns 404 if role not found.
public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result>
{
    private readonly IRoleRepository roleRepo;

    public UpdateRoleCommandHandler(IRoleRepository roleRepo)
    {
        this.roleRepo = roleRepo;
    }

    public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var existRole = await roleRepo.GetAsync(_ => _.Id.Equals(request.UpdateRole.Id));

        if (existRole is null)
            return Result.Failure(404);

        existRole.RoleName = request.UpdateRole.RoleName ?? default!;
        existRole.Description = request.UpdateRole.Description ?? default!;

        await roleRepo.Update(existRole);
        return Result.Success(204);
    }
}

/*
This code handles the request to update an existing role in the system.

It takes in an UpdateRoleCommand object as input, which contains the updated role information to persist.

It returns a Result object indicating success or failure of the update.

The main logic is:

1. Get the existing role entity from the database using the role repository and the ID in the request.

2. Check if the role exists. If not, return a 404 failure result.

3. Update the name, description and other properties of the existing role object from the request model.

4. Call the role repository to persist the updated role entity to the database.

5. Return a successful 204 result if updated successfully, indicating the role was updated.

So in summary, it takes a role update request, validates the role exists, updates the database entity, and returns a result indicating success or failure to the caller. The main logic is retrieving the existing entity, validating it exists, updating its properties, persisting changes, and returning the outcome.





*/