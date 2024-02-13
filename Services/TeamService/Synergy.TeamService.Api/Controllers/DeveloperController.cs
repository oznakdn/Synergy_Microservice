using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.TeamService.Application.Commands.AddDeveloperSkill;
using Synergy.TeamService.Application.Commands.CreateDeveloper;
using Synergy.TeamService.Application.Queries.GetDeveloperDetails;
using Synergy.TeamService.Application.Queries.GetDevelopers;
using Synergy.TeamService.Application.Queries.GetDevelopersByTeamId;
using Synergy.TeamService.Shared.Dtos.DeveloperDtos;
using Synergy.TeamService.Shared.Dtos.DeveloperSkillDtos;
using System.Security.Claims;

namespace Synergy.TeamService.Api.Controllers;

[Route("api/developers")]
[ApiController]
[Authorize(Roles = "manager")]
public class DeveloperController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetDevelopers()
    {
        var result = await mediator.Send(new GetDevelopersQuery());
        return Ok(result.Values);
    }

    [HttpGet("{teamId}")]
    public async Task<IActionResult> GetDevelopersByTeamId(string teamId)
    {
        var result = await mediator.Send(new GetDevelopersByTeamIdQuery(teamId));
        return Ok(result.Values);
    }

    [HttpGet("details/{developerId}")]
    public async Task<IActionResult> GetDeveloperDetails(string developerId)
    {
        var result = await mediator.Send(new GetDeveloperDetailsQuery(developerId));
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDeveloper([FromBody] CreateDeveloperDto createDeveloper)
    {
        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new CreateDeveloperCommand
        {
            CreateDeveloper = createDeveloper,
            CreatedBy = createdBy
        });

        return Ok();
    }

    [HttpPost("skill")]
    public async Task<IActionResult> AddDeveloperSkill([FromBody] AddDeveloperSkillDto addDeveloperSkill)
    {
        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new AddDeveloperSkillCommand
        {
            AddDeveloperSkill = addDeveloperSkill,
            CreatedBy = createdBy
        });

        return Ok();
    }
}
