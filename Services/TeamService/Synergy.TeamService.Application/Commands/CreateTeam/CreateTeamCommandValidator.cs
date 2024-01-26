using FluentValidation;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Commands.CreateTeam;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamDto>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(_ => _.TeamName).NotEmpty().NotNull().Length(3,15);
        RuleFor(_=> _.TeamDescription).NotEmpty().NotNull().Length(5, 50);
    }
}
