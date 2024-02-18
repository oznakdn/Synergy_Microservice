using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Queries.GetTeams;

public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, Result<TeamDto>>
{
    private readonly IRepositoryManager _manager;

    public GetTeamsQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result<TeamDto>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
    {
        var query = await _manager.Team.GetAsync(includes: _ => _.Members);

        var teams = await query.ToListAsync(cancellationToken);
        var teamDto = teams.Select(x=> new TeamDto(x.Id.ToString(),x.TeamName,x.TeamDescription)).ToList();
        return Result<TeamDto>.Success(statusCode: 200, values: teamDto);
    }
}
