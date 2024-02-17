using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IResult<UserDto>>
{
    private readonly IUserRepository _userRepo;

    public GetUserByIdQueryHandler(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<IResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetAsync(x => x.Id == request.UserId, cancellationToken);

        if (user is null)
            return Result<UserDto>.Failure(404);


        var result = new UserDto(user.Id, user.Username, user.Email, user.Role!.RoleName);
        return Result<UserDto>.Success(value: result, statusCode: 200);
    }
}
