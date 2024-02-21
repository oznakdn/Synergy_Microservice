using MediatR;
using Synergy.ProjectService.Shared.Dtos.CaseDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Queries.GetCasesByProjectId;

public class GetCasesByProjectIdQuery : IRequest<IResult<CaseDto>>
{
    public GetCasesByProjectIdQuery(string projectId)
    {
        ProjectId = projectId;
    }

    public string ProjectId { get; set; }   
}
