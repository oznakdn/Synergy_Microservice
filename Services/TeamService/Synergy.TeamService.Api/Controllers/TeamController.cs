using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.TeamService.Application.Commands.AssignMemberToTeam;
using Synergy.TeamService.Application.Commands.CreateTeam;
using Synergy.TeamService.Application.Commands.DeleteTeam;
using Synergy.TeamService.Application.Commands.UpdateTeam;
using Synergy.TeamService.Application.Queries.GetTeam;
using Synergy.TeamService.Application.Queries.GetTeams;
using Synergy.TeamService.Shared.Dtos.TeamDtos;
using System.Security.Claims;

namespace Synergy.TeamService.Api.Controllers;

[Route("api/teams")]
[ApiController]
[Authorize(Roles = "manager")]
public class TeamController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetTeams()
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


    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto createTeam)
    {

        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new CreateTeamCommand(createTeam, createdBy));
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }

    [HttpPut("assignmember")]
    [AllowAnonymous]
    public async Task<IActionResult> AssignMember([FromBody] AssignMemberDto assignMember)
    {

        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new AssignMemberCommand(assignMember, createdBy));
        return result.StatusCode == 404 ? NotFound(result.Message) : result.StatusCode == 400 ? BadRequest() : Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamDto updateTeam)
    {
        string updatedBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new UpdateTeamCommand(updateTeam, updatedBy));

        return result.StatusCode == 404 ? NotFound() : result.StatusCode == 400 ? BadRequest() : Ok();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeam(string id)
    {

        string deletedBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new DeleteTeamCommand(id, deletedBy));

        return result.StatusCode == 404 ? NotFound() : result.StatusCode == 400 ? BadRequest() : Ok();
    }



}
