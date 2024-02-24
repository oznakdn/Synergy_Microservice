using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUserByMemberId;

/// Handles GetUserByMemberId queries by retrieving the user with the given member ID from the repository.
/// Returns a UserDto object containing user details if found, otherwise returns a 404 failure result.
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

/*
GetUserByMemberIdQueryHandler is a class that handles queries to get a user by their member ID.

It takes a GetUserByMemberIdQuery as input, which contains the member ID to search for.

It returns an IResult as output. This will contain a UserDto object with information about the found user, or an error result if no user was found.

The main logic is:

It injects an IUserRepository in the constructor to interact with user data.

The Handle method is called when a GetUserByMemberIdQuery comes in.

It calls the repository's GetAsync method, passing a filter to find a user where the MemberId property matches the query's MemberId.

If no user is found, it returns a 404 error result.

If a user is found, it creates a UserDto object containing the Id, Username, Email, MemberId, and RoleName from the user entity.

It returns a success result containing the UserDto.

So in summary, it takes a member ID, queries the user repository to find a matching user, and returns a DTO containing user data or an error if not found. This allows querying user info by member ID while encapsulating the data access and mapping logic.
*/