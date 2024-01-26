using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.DeveloperDtos;

namespace Synergy.TeamService.Application.Queries.GetDevelopers;

public class GetDevelopersQuery : IRequest<Result<DeveloperDto>>
{
}
