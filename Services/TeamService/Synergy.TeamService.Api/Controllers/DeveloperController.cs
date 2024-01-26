﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Synergy.TeamService.Application.Commands.AddDeveloperSkill;
using Synergy.TeamService.Application.Commands.CreateDeveloper;
using Synergy.TeamService.Application.Queries.GetDevelopers;
using Synergy.TeamService.Shared.Dtos.DeveloperDtos;
using Synergy.TeamService.Shared.Dtos.DeveloperSkillDtos;
using System.Security.Claims;

namespace Synergy.TeamService.Api.Controllers;

[Route("api/developers")]
[ApiController]
public class DeveloperController(IMediator mediator) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult>GetDevelopers()
    {
        var result = await mediator.Send(new GetDevelopersQuery());
        return Ok(result.Values);
    }

    [HttpPost]
    [Authorize(Roles ="admin")]
    public async Task<IActionResult> CreateDeveloper([FromBody] CreateDeveloperDto createDeveloper)
    {
        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new CreateDeveloperCommand
        {
            CreateDeveloper = createDeveloper,
            CreatedBy = createdBy
        });

        return Ok();
    }

    [HttpPost("skill")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> AddDeveloperSkill([FromBody] AddDeveloperSkillDto addDeveloperSkill)
    {
        string createdBy = User.FindFirst(_ => _.Type == ClaimTypes.Name)!.Value;
        var result = await mediator.Send(new AddDeveloperSkillCommand
        {
           AddDeveloperSkill = addDeveloperSkill,
            CreatedBy = createdBy
        });

        return Ok();
    }
}
