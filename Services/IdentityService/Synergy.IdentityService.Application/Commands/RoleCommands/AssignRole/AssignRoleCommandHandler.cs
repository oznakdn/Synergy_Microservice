using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.RoleCommands.AssignRole;

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
