using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.DeveloperDtos;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Queries.GetDevelopersByTeamId;

public class GetDevelopersByTeamIdQuery : IRequest<Result<DeveloperDto>>
{
    public GetDevelopersByTeamIdQuery(string teamId)
    {
        TeamId = teamId;
    }

    public string TeamId { get; set; }
}
