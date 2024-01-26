using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.TechnologyDtos;

namespace Synergy.TeamService.Application.Queries.GetTechnologies;

public class GetTechnologiesQuery : IRequest<Result<TechnologyDto>>
{
}
