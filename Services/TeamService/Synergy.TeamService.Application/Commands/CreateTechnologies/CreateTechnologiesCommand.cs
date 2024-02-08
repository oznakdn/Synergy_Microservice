using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.TechnologyDtos;

namespace Synergy.TeamService.Application.Commands.CreateTechnologies;

public class CreateTechnologiesCommand : IRequest<Result>
{
    public CreateTechnologiesCommand(CreateTechnologyDto createTechnology)
    {
        CreateTechnology = createTechnology;
    }

    //public List<CreateTechnologyDto>? CreateTechnologies { get; set; }
    public CreateTechnologyDto CreateTechnology { get; set; }

}
