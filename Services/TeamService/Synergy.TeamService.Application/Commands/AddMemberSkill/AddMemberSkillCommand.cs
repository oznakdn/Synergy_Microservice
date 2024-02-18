using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.SkillDtos;

namespace Synergy.TeamService.Application.Commands.AddMemberSkill;

public class AddMemberSkillCommand : IRequest<Result>
{
    public AddMemberSkillDto AddDeveloperSkill { get; set; }
    public string CreatedBy { get; set; }   
}
