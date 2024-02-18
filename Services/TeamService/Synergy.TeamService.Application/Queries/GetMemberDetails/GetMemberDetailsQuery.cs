using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.MemberDtos;

namespace Synergy.TeamService.Application.Queries.GetMemberDetails;

public class GetMemberDetailsQuery : IRequest<IResult<MemberDetailsDto>>
{
    public GetMemberDetailsQuery(string developerId)
    {
        DeveloperId = developerId;
    }

    public string DeveloperId { get; set; }
}
