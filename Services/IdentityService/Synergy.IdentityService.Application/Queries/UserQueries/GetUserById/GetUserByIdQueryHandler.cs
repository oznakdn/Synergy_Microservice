using MediatR;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUserById;

/// Handles GetUserById queries by retrieving the user with the given ID from the user repository.
/// Returns a UserDto object containing the user details if found, otherwise returns a 404 not found result.
/// The UserDto will contain the user's role name if a role is assigned.
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


        if (user.Role is not null)
        {
            var resultWithRole = new UserDto(user.Id, user.Username, user.Email,user.MemberId, user.Role != null ? user.Role.RoleName : string.Empty);
            return Result<UserDto>.Success(value: resultWithRole, statusCode: 200);

        }

        var result = new UserDto(user.Id, user.Username, user.Email, string.Empty);
        return Result<UserDto>.Success(value: result, statusCode: 200);
    }
}


/*
The GetUserByIdQueryHandler class is designed to retrieve a user record from the database by its ID and return a UserDto object containing information about that user.

It takes a GetUserByIdQuery object as input, which contains the ID of the user to retrieve.

It returns an IResult object, which wraps the UserDto result and also contains a status code indicating whether the operation succeeded or failed.

The main logic flow is:

1. In the constructor, it accepts an IUserRepository dependency which it uses to query the database.

2. The Handle method first calls the repository to get the user by ID.

3. It checks if the user is null, and if so, returns a 404 failure result.

4. If a user is found, it checks if the user has a role assigned.

5. If there is a role, it creates a UserDto, populating the role name from the user.Role object.

6. If no role, it creates the UserDto without a role name.

7. In both cases, it returns a success result containing the UserDto.

So in summary, it takes a user ID, queries the database, handles any errors, 
transforms the domain user object to a DTO, and returns that DTO wrapped in a result 
object indicating success or failure. This allows cleanly returning either the desired 
user data or an error status code.
*/