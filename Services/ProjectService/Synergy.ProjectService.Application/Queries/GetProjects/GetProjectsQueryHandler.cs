using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;
using Synergy.ProjectService.Shared.Dtos.ProjectDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Queries.GetProjects;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, Result<ProjectDto>>
{
    private readonly IRepositoryManager _manager;

    public GetProjectsQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projectQuery = await _manager.Project.GetAsync();
        var projects = await projectQuery.ToListAsync();

        var result = projects.Select(x => new ProjectDto(
            x.Id,
            x.Title,
            x.Description,
            x.ProjectStatus.ToString(),
            x.StartDate.ToShortDateString(),
            x.EndDate.ToShortDateString(),
            x.TeamId));

        return Result<ProjectDto>.Success(statusCode: 200,values: result);
    }
}
