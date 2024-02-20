using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Application.Commands.UpdateTeam;

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, IResult>
{
    private readonly IRepositoryManager _manager;

    public UpdateTeamCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<IResult> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var teamQuery = await _manager.Team.GetAsync(_ => _.Id == Guid.Parse(request.UpdateTeam.Id));

        if (!teamQuery.Any())
        {
            return Result.Failure(404);
        }

        var team = await teamQuery.SingleOrDefaultAsync(cancellationToken);

        team.TeamName = request.UpdateTeam.TeamName ?? default!;
        team.TeamDescription = request.UpdateTeam.TeamDescription ?? default!;
        team.ModifiedBy = request.UpdatedBy;

        _manager.Team.Update(team);
        var result = await _manager.SaveAsync(cancellationToken);

        if (result == 0)
            return Result.Failure(400);

        return Result.Success(200);

    }
}
