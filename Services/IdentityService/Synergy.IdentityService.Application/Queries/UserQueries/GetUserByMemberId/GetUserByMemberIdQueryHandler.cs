using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUserByMemberId;

public class GetUserByMemberIdQueryHandler : IRequestHandler<GetUserByMemberIdQuery, IResult<UserDto>>
{
    private readonly IUserRepository userRepo;

    public GetUserByMemberIdQueryHandler(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    public async Task<IResult<UserDto>> Handle(GetUserByMemberIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepo.GetAsync(_ => _.MemberId == request.MemberId);
        if (user is null)
            return Result<UserDto>.Failure(404);

        var userDto = new UserDto(user.Id, user.Username, user.Email, user.MemberId, user.Role != null ? user.Role.RoleName : string.Empty);
        return Result<UserDto>.Success(value: userDto);
    }
}
