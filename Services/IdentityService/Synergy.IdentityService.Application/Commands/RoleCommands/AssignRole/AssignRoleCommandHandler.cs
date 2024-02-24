using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.RoleCommands.AssignRole;

/// Handles assigning a role to a user.
/// Fetches the user and role from their repositories.
/// Returns 404 if either user or role don't exist.
/// Updates user with new role if different from current role.
/// Returns 204 if role updated, 400 if user already has role.
public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Result>
{
    private readonly IUserRepository userRepo;
    private readonly IRoleRepository roleRepo;

    public AssignRoleCommandHandler(IUserRepository userRepo, IRoleRepository roleRepo)
    {
        this.userRepo = userRepo;
        this.roleRepo = roleRepo;
    }

    public async Task<Result> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetAsync(_ => _.Id.Equals(request.UserId));
        if (user is null)
            return Result.Failure(404);

        var role = await roleRepo.GetAsync(_ => _.Id.Equals(request.RoleId));
        if (role is null)
            return Result.Failure(404);

        if (user.Role is null)
            user.Role = new();

        if (user.Role.RoleName != role.RoleName)
        {
            user.Role = role;
            await userRepo.Update(user);
            return Result.Success(204);
        }

        return Result.Failure(400,error: "User already has this role!");
    }
}

/*
The AssignRoleCommandHandler class handles assigning a role to a user.

It takes in an AssignRoleCommand object as input, which contains the ID of the user and role to assign.

It returns a Result object, which indicates success or failure of the role assignment.

The class first fetches the User and Role objects corresponding to the IDs in the command using repository objects. If either object is not found, it returns a 404 failure result.

If the user does not already have a role, it creates a new empty role object on the user.

It then checks if the user's current role name matches the role name of the role object from the command.

If they differ, it assigns the role object from the command to the user, updates the user in the repository, and returns a 204 success result.

If the role names match, it means the user already has that role, so it returns a 400 failure result with an error message.

Overall, this class handles the business logic of assigning a role to a user - validating the role and user exist, checking the current role, updating if needed, and returning appropriate success or error results. The main logic flow is fetching data, validating, updating if valid, and responding with correct result status code.

*/