using MediatR;
using Synergy.Shared.Results;

namespace Synergy.TeamService.Application.Commands.DeleteTeam;

public class DeleteTeamCommand : IRequest<IResult>
{
    public DeleteTeamCommand(string id, string deletedBy)
    {
        Id = id;
        DeletedBy = deletedBy;
    }

    public string Id { get; set; }
    public string DeletedBy { get; set; }
}
