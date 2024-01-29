using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.ProjectService.Application.Commands.CreateProject;
using Synergy.ProjectService.Application.Queries.GetProjects;
using Synergy.ProjectService.Shared.Dtos.ProjectDtos;
using System.Security.Claims;

namespace Synergy.ProjectService.Api.Controllers;

[Route("api/projects")]
[ApiController]
public class ProjectController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult>GetProjects()
    {
        var result = await mediator.Send(new GetProjectsQuery());
        return Ok(result.Values);
    }

    [HttpPost]
    [Authorize(Roles ="admin")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto createProject)
    {
        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new CreateProjectCommand(createProject, createdBy));
        return result.IsSuccess ? Ok() : BadRequest(result);
    }
}
