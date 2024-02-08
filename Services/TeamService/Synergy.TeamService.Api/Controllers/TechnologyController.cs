using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.Shared.Constants;
using Synergy.TeamService.Application.Commands.CreateTechnologies;
using Synergy.TeamService.Application.Queries.GetTechnologies;
using Synergy.TeamService.Shared.Dtos.TechnologyDtos;

namespace Synergy.TeamService.Api.Controllers;

[Route("api/technologies")]
[ApiController]
[Authorize(Roles = RoleConstant.MANAGER)]
public class TechnologyController(IMediator mediator) : ControllerBase
{

    //[HttpPost]
    //public async Task<IActionResult> CreateTechnologies([FromBody] List<CreateTechnologyDto> createTechnology)
    //{
    //    var result = await mediator.Send(new CreateTechnologiesCommand { CreateTechnology = createTechnology });
    //    return result.IsSuccess ? NoContent() : BadRequest(result.Message);
    //}

    [HttpPost]
    public async Task<IActionResult> CreateTechnology([FromBody] CreateTechnologyDto createTechnology)
    {
        var result = await mediator.Send(new CreateTechnologiesCommand(createTechnology));
        return result.IsSuccess ? NoContent() : BadRequest(result.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetTechnologies()
    {
        var result = await mediator.Send(new GetTechnologiesQuery());
        return Ok(result.Values);
    }
}
