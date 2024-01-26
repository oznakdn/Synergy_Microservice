﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Queries.GetTeam;

public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, Result<TeamDto>>
{
    private readonly IRepositoryManager _manager;

    public GetTeamQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result<TeamDto>> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        var query = await _manager.Team.GetAsync(filter: _ => _.Id == Guid.Parse(request.TeamId), includes: _ => _.Developers);
        var team = await query.SingleOrDefaultAsync();

        if (team is null)
            return (Result<TeamDto>)Result<TeamDto>.Failure(404);

        var teamDto = new TeamDto(team.Id.ToString(),
            team.TeamName,
            team.TeamDescription,
            team.Developers.Select(_=> new TeamDevelopers(_.GivenName,_.LastName,_.Title)).ToList());

        return Result<TeamDto>.Success(200, teamDto); 
    }
}
