using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Queries.GetTeams;

public class GetTeamsQuery : IRequest<Result<TeamDto>>
{

}
