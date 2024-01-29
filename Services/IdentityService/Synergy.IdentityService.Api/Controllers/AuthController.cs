using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.IdentityService.Application.Commands.RoleCommands.AssignRole;
using Synergy.IdentityService.Application.Commands.RoleCommands.CreateRole;
using Synergy.IdentityService.Application.Commands.RoleCommands.UpdateRole;
using Synergy.IdentityService.Application.Queries.RoleQueries.GetRoles;
using Synergy.IdentityService.Application.Queries.UserQueries.GetUsers;
using Synergy.IdentityService.Shared.Dtos.RoleDtos;

namespace Synergy.IdentityService.Api.Controllers;

[Route("api/auth")]
[ApiController]
[Authorize(Roles = "manager")]

public class AuthController(IMediator mediator) : ControllerBase
{

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await mediator.Send(new GetUsersQuery());
        return Ok(result.Values);
    }


    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var result = await mediator.Send(new GetRolesQuery());
        return Ok(result.Values);
    }

    [HttpPost("roles")]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto createRole)
    {
        var result = await mediator.Send(new CreateRoleCommand { CreateRole = createRole });
        return result.IsSuccess ? Ok(result.StatusCode) : BadRequest(result.Errors);
    }

    [HttpPut("roles")]
    public async Task<IActionResult> Update([FromBody] UpdateRoleDto updateRole)
    {
        var result = await mediator.Send(new UpdateRoleCommand(updateRole));
        return result.IsSuccess ? Ok(result.StatusCode) : BadRequest(result.Errors);
    }


    [HttpPut("roles/assign")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand assignRole)
    {
        var result = await mediator.Send(assignRole);
        return result.IsSuccess ? Ok(result.StatusCode) : BadRequest(result.Errors);
    }


}
