using MediatR;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUsers;

public class GetUsersQuery:IRequest<Result<UserDto>>
{

}
