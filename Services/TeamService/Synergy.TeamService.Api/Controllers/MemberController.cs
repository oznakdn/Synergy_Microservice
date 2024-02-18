using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.TeamService.Application.Commands.AddMemberSkill;
using Synergy.TeamService.Application.Commands.CreateMember;
using Synergy.TeamService.Application.Queries.GetMemberDetails;
using Synergy.TeamService.Application.Queries.GetMembers;
using Synergy.TeamService.Application.Queries.GetMembersByTeamId;
using Synergy.TeamService.Shared.Dtos.MemberDtos;
using Synergy.TeamService.Shared.Dtos.SkillDtos;
using System.Security.Claims;

namespace Synergy.TeamService.Api.Controllers;

[Route("api/members")]
[ApiController]
[Authorize(Roles = "manager")]
public class MemberController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetDevelopers()
    {
        var result = await mediator.Send(new GetMembersQuery());
        return Ok(result.Values);
    }

    [HttpGet("{teamId}")]
    public async Task<IActionResult> GetDevelopersByTeamId(string teamId)
    {
        var result = await mediator.Send(new GetMembersByTeamIdQuery(teamId));
        return Ok(result.Values);
    }

    [HttpGet("details/{developerId}")]
    public async Task<IActionResult> GetDeveloperDetails(string developerId)
    {
        var result = await mediator.Send(new GetMemberDetailsQuery(developerId));
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateDeveloper([FromBody] CreateMemberDto createDeveloper)
    {
        // TODO: Buraya istek geldiginde message produce edecek ve identity service register consumer'i tetikleyecek.
        // TODO: CreateDeveloperDto degisecek. Icerisinde register icin gerekli olan propertiler de olacak (username, email, password) 
        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new CreateMemberCommand
        {
            CreateMember = createDeveloper,
            CreatedBy = createdBy
        });

        return Ok();
    }

    [HttpPost("skill")]
    public async Task<IActionResult> AddDeveloperSkill([FromBody] AddMemberSkillDto addDeveloperSkill)
    {
        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new AddMemberSkillCommand
        {
            AddDeveloperSkill = addDeveloperSkill,
            CreatedBy = createdBy
        });

        return Ok();
    }
}
