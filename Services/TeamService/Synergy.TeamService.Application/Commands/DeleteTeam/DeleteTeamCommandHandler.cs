using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Application.Commands.DeleteTeam;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, IResult>
{
    private readonly IRepositoryManager _manager;

    public DeleteTeamCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<IResult> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var teamQuery = await _manager.Team.GetAsync(_ => _.Id == Guid.Parse(request.Id));

        if(!teamQuery.Any()) 
        {
            return Result.Failure(404);
        }

        var team = await teamQuery.SingleOrDefaultAsync(cancellationToken);
        // TODO : Modellere IsActive property'si eklenerek softdelete metodu olusturulacak
        _manager.Team.Delete(team);
        var result = await _manager.SaveAsync(cancellationToken);

        if(result == 0)
        {
            return Result.Failure(400);
        }

        return Result.Success(200);
    }
}
