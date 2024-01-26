using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.DeveloperDtos;

namespace Synergy.TeamService.Application.Commands.CreateDeveloper;

public class CreateDeveloperCommand : IRequest<Result>
{
    public CreateDeveloperDto CreateDeveloper { get; set; }
    public string CreatedBy { get; set; }
}
