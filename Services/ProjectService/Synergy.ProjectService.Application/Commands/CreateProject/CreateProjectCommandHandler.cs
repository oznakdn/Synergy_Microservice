using MediatR;
using Synergy.ProjectService.Domain.Models;
using Synergy.ProjectService.Domain.Models.Enums;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Commands.CreateProject;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Result>
{
    private readonly IRepositoryManager _manager;

    public CreateProjectCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var projectQuery = await _manager.Project.GetAsync(filter: _ => _.Title.ToLower() == request.CreateProject.Title.ToLower());

        if (projectQuery.Any())
            return Result.Failure(400, "Project is already exist!");

        var project = new Project
        {
            Title = request.CreateProject.Title,    
            Description = request.CreateProject.Description,
            CreatedDate = DateTime.Now,
            CreatedBy = request.CreatedBy,
            StartDate = request.CreateProject.StartDate,
            EndDate = request.CreateProject.EndDate,
            ProjectStatus = (Status)request.CreateProject.ProjectStatus,
            TeamId = request.CreateProject.TeamId
        };

        _manager.Project.Insert(project);
        var result = await _manager.SaveAsync();

        if (result > 0)
            return Result.Success(204);

        return Result.Failure(500);
    }
}
