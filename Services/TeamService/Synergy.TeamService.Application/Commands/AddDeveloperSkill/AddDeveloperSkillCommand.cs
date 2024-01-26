using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.DeveloperSkillDtos;

namespace Synergy.TeamService.Application.Commands.AddDeveloperSkill;

public class AddDeveloperSkillCommand : IRequest<Result>
{
    public AddDeveloperSkillDto AddDeveloperSkill { get; set; }
    public string CreatedBy { get; set; }   
}
