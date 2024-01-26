using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.RoleCommands.UpdateRole;

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