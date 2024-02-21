using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.ProjectService.Application.Commands.CreateCase;
using Synergy.ProjectService.Application.Queries.GetCasesByMemberId;
using Synergy.ProjectService.Application.Queries.GetCasesByProjectId;
using Synergy.ProjectService.Shared.Dtos.CaseDtos;
using System.Security.Claims;

namespace Synergy.ProjectService.Api.Controllers;

[Route("api/cases")]
[ApiController]
[Authorize]
public class CaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public CaseController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("member/{memberId}")]
    public async Task<IActionResult> GetCasesByMemberId(string memberId)
    {
        var result = await _mediator.Send(new GetCasesByMemberIdQuery(memberId));
        return Ok(result);
    }


    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetCasesByProjectId(string projectId)
    {
        var result = await _mediator.Send(new GetCasesByProjectIdQuery(projectId));
        return Ok(result);
    }


    [HttpPost]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> CreateCase([FromBody] CreateCaseDto createCase)
    {
        var createdBy = User.Claims.First(_ => _.Type == ClaimTypes.Name).Value;
        var result = await _mediator.Send(new CreateCaseCommand(createCase, createdBy));
        return result.StatusCode == 400 ? BadRequest() : NoContent();
    }
}
