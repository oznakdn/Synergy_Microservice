using MediatR;
using Synergy.ProjectService.Shared.Dtos.CaseDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Queries.GetCasesByMemberId;

public class GetCasesByMemberIdQuery : IRequest<IResult<CaseDto>>
{
    public GetCasesByMemberIdQuery(string memberId)
    {
        MemberId = memberId;
    }

    public string MemberId { get; set; }
}
