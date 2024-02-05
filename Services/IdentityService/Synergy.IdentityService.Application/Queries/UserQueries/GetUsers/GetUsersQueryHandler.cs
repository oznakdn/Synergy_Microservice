using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<UserDto>>
{
    private readonly IUserRepository userRepo;

    public GetUsersQueryHandler(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    public async Task<Result<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepo.GetAllAsync();
        var usersDto = users
            .Select(_ => new UserDto(_.Id, _.Username, _.Email, _.Role is not null ? _.Role.RoleName : default))
            .ToList();

        return Result<UserDto>.Success(statusCode: 200, values: usersDto);
    }
}
