using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Queries.GetTeam;

public class GetTeamQuery : IRequest<Result<TeamDto>>
{
    public GetTeamQuery(string teamId)
    {
        TeamId = teamId;
    }

    public string TeamId { get; set; }
}
