using MediatR;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<User>>
{
    private readonly IUserRepository userRepo;

    public GetUsersQueryHandler(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    public async Task<Result<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepo.GetAllAsync();
        return Result<User>.Success(200, users);
    }
}
