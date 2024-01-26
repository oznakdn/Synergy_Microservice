using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Commands.RoleCommands.CreateRole;

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
            return Result.Failure(400, new List<string> { "Role is already exist!" });

        await roleRepo.CreateAsync(new Domain.Models.Role
        {
            RoleName = request.CreateRole.RoleName,
            Description = request.CreateRole.Description
        });

        return Result.Success(200);
    }
}
