using MediatR;
using Microsoft.AspNetCore.Mvc;
using Synergy.IdentityService.Application.Commands.UserCommands.RegisterUser;
using Synergy.IdentityService.Application.Queries.UserQueries.LoginUser;
using Synergy.IdentityService.Shared.Dtos.UserDtos;

namespace Synergy.IdentityService.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IMediator _mediator) : ControllerBase
{


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto register)
    {
        var result = await _mediator.Send(new RegisterUserCommand
        {
            Register = register
        });
        return result.IsSuccess ? Ok(result) : BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var result = await _mediator.Send(new LoginUserQuery
        {
            Login = login
        });
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
}
