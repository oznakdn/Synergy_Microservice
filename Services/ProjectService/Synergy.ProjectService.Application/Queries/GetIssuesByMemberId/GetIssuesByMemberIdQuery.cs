using MediatR;
using Synergy.ProjectService.Shared.Dtos.IssueDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Queries.GetIssuesByMemberId;

public class GetIssuesByMemberIdQuery : IRequest<IResult<IssueDto>>
{
    public GetIssuesByMemberIdQuery(string memberId)
    {
        MemberId = memberId;
    }

    public string MemberId { get; set; }
}
