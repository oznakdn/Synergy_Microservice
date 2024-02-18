using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.MemberDtos;

namespace Synergy.TeamService.Application.Commands.CreateMember;

public class CreateMemberCommand : IRequest<Result>
{
    public CreateMemberDto CreateMember { get; set; }
    public string CreatedBy { get; set; }
}
