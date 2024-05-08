using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.ProjectService.Application.Commands.CreateIssue;
using Synergy.ProjectService.Application.Queries.GetIssuesByMemberId;
using Synergy.ProjectService.Shared.Dtos.IssueDtos;
using System.Security.Claims;

namespace Synergy.ProjectService.Api.Controllers;

[Route("api/cases")]
[ApiController]
[Authorize]
public class IssueController : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("member/{memberId}")]
    public async Task<IActionResult> GetIssuesByMemberId(string memberId)
    {
        var result = await _mediator.Send(new GetIssuesByMemberIdQuery(memberId));
        return Ok(result);
    }


    //[HttpGet("project/{projectId}")]
    //public async Task<IActionResult> GetCasesByProjectId(string projectId)
    //{
    //    var result = await _mediator.Send(new GetIss(projectId));
    //    return Ok(result);
    //}


    [HttpPost]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> CreateIssue([FromBody] CreateIssueDto createIssue)
    {
        var createdBy = User.Claims.First(_ => _.Type == ClaimTypes.Name).Value;
        var result = await _mediator.Send(new CreateIssueCommand(createIssue, createdBy));
        return result.StatusCode == 400 ? BadRequest() : NoContent();
    }
}
