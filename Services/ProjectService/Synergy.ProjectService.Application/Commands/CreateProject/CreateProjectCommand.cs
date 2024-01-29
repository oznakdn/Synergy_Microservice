using MediatR;
using Synergy.ProjectService.Shared.Dtos.ProjectDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Commands.CreateProject;

public class CreateProjectCommand:IRequest<Result>
{
    public CreateProjectCommand(CreateProjectDto createProject, string createdBy)
    {
        CreateProject = createProject;
        CreatedBy = createdBy;
    }

    public CreateProjectDto CreateProject { get; set; }
    public string CreatedBy { get; set; }
}
