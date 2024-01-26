using MediatR;
using Microsoft.AspNetCore.Mvc;
using Synergy.TeamService.Application.Commands.CreateTechnologies;
using Synergy.TeamService.Application.Queries.GetTechnologies;
using Synergy.TeamService.Shared.Dtos.TechnologyDtos;

namespace Synergy.TeamService.Api.Controllers;

[Route("api/technologies")]
[ApiController]
public class TechnologyController(IMediator mediator) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateTechnologies([FromBody] List<CreateTechnologyDto> createTechnology)
    {
        var result = await mediator.Send(new CreateTechnologiesCommand { CreateTechnologies = createTechnology });
        return result.IsSuccess ? NoContent() : BadRequest(result.Message);
    }

    [HttpGet]
    public async Task<IActionResult>GetTechnologies()
    {
        var result = await mediator.Send(new GetTechnologiesQuery());
        return Ok(result.Values);
    }
}
