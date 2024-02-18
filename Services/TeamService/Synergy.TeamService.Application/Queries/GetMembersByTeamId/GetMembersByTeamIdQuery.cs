using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.MemberDtos;

namespace Synergy.TeamService.Application.Queries.GetMembersByTeamId;

public class GetMembersByTeamIdQuery : IRequest<Result<MemberDto>>
{
    public GetMembersByTeamIdQuery(string teamId)
    {
        TeamId = teamId;
    }

    public string TeamId { get; set; }
}
