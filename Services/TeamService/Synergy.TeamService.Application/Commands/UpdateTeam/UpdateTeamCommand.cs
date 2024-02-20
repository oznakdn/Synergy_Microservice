using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Commands.UpdateTeam;

public class UpdateTeamCommand : IRequest<IResult>
{
    public UpdateTeamCommand(UpdateTeamDto updateTeam, string updatedBy)
    {
        UpdateTeam = updateTeam;
        UpdatedBy = updatedBy;
    }

    public UpdateTeamDto UpdateTeam { get; set; }
    public string UpdatedBy { get; set; }
}
