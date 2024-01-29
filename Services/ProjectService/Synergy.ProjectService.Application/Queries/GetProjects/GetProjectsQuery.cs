using MediatR;
using Synergy.ProjectService.Shared.Dtos.ProjectDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Queries.GetProjects;

public class GetProjectsQuery : IRequest<Result<ProjectDto>>
{

}
