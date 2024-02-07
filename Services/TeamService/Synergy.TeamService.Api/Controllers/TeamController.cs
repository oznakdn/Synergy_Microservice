using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.TeamService.Application.Commands.CreateTeam;
using Synergy.TeamService.Application.Queries.GetTeam;
using Synergy.TeamService.Application.Queries.GetTeams;
using Synergy.TeamService.Shared.Dtos.TeamDtos;
using System.Security.Claims;

namespace Synergy.TeamService.Api.Controllers;

[Route("api/teams")]
[ApiController]
[Authorize(Roles ="manager")]
public class TeamController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto createTeam)
    {

        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new CreateTeamCommand(createTeam, createdBy));
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [HttpGet]
    public async Task<IActionResult>GetTeams()
    {
        var result = await mediator.Send(new GetTeamsQuery());
        return Ok(result.Values);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeamById(string id)
    {
        var result = await mediator.Send(new GetTeamQuery(id));
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }


}
