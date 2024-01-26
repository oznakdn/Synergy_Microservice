using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Commands.CreateTeam;

public class CreateTeamCommand : IRequest<Result>
{
    public CreateTeamCommand(CreateTeamDto createTeam, string createdBy)
    {
        CreateTeam = createTeam;
        CreatedBy = createdBy;
    }

    public CreateTeamDto CreateTeam { get; set; }
    public string  CreatedBy { get; set; }
}
