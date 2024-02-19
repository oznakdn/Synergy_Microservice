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
    public async Task<IActionResult> GetMembers()
    {
        var result = await mediator.Send(new GetMembersQuery());
        return Ok(result.Values);
    }

    [HttpGet("{teamId}")]
    public async Task<IActionResult> GetMembersByTeamId(string teamId)
    {
        var result = await mediator.Send(new GetMembersByTeamIdQuery(teamId));
        return Ok(result.Values);
    }

    [HttpGet("details/{memberId}")]
    public async Task<IActionResult> GetMemberDetails(string memberId)
    {
        var result = await mediator.Send(new GetMemberDetailsQuery(memberId));
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateMember([FromBody] CreateMemberDto createMember)
    {
        var result = await mediator.Send(new CreateMemberCommand
        {
            CreateMember = createMember,
            CreatedBy =  createMember.CreateUser.Username
        });

        return Ok();
    }

    [HttpPost("skill")]
    public async Task<IActionResult> AddMemberSkill([FromBody] AddMemberSkillDto addMemberSkill)
    {
        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new AddMemberSkillCommand
        {
            AddDeveloperSkill = addMemberSkill,
            CreatedBy = createdBy
        });

        return result.IsSuccess ? Ok() : BadRequest();
    }
}
