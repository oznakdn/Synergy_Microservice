using MediatR;
using Synergy.IdentityService.Shared.Dtos.UserDtos;
using Synergy.Shared.Results;

namespace Synergy.IdentityService.Application.Queries.UserQueries.GetUserByMemberId;

public class GetUserByMemberIdQuery : IRequest<IResult<UserDto>>
{
    public GetUserByMemberIdQuery(string memberId)
    {
        MemberId = memberId;
    }

    public string MemberId { get; set; }

}
