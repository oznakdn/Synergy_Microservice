using MediatR;
using Synergy.ProjectService.Shared.Dtos.IssueDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Commands.CreateIssue;

public class CreateIssueCommand : IRequest<IResult>
{
    public CreateIssueCommand(CreateIssueDto createIssue, string createdBy)
    {
        CreateIssue = createIssue;
        CreatedBy = createdBy;
    }

    public CreateIssueDto CreateIssue { get; set; }
    public string CreatedBy { get; set; }


}
