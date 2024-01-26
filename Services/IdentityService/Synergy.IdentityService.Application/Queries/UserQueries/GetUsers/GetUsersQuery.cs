using MediatR;
using Synergy.IdentityService.Domain.Models;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUsers;

public class GetUsersQuery:IRequest<Result<User>>
{

}
