using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Commands.AssignMemberToTeam;

public class AssignMemberCommand : IRequest<IResult>
{
    public AssignMemberCommand(AssignMemberDto assignMember, string updatedBy)
    {
        AssignMember = assignMember;
        UpdatedBy = updatedBy;
    }

    public AssignMemberDto AssignMember { get; set; }
    public string UpdatedBy { get; set; }
}
