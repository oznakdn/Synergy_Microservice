using MediatR;
using Synergy.ProjectService.Shared.Dtos.CaseDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Commands.CreateCase;

public class CreateCaseCommand : IRequest<IResult>
{
    public CreateCaseCommand(CreateCaseDto createCase, string createdBy)
    {
        CreateCase = createCase;
        CreatedBy = createdBy;
    }

    public CreateCaseDto CreateCase { get; set; }
    public string CreatedBy { get; set; }

}
