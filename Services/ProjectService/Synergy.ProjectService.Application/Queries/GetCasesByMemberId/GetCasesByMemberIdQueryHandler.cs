using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.ProjectService.Domain.Models;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;
using Synergy.ProjectService.Shared.Dtos.CaseDtos;
using Synergy.ProjectService.Shared.Dtos.CommentDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Queries.GetCasesByMemberId;

public class GetCasesByMemberIdQueryHandler : IRequestHandler<GetCasesByMemberIdQuery, IResult<CaseDto>>
{
    private readonly IRepositoryManager _manager;

    public GetCasesByMemberIdQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<IResult<CaseDto>> Handle(GetCasesByMemberIdQuery request, CancellationToken cancellationToken)
    {
        var caseQuery = await _manager.Case.GetAsync(_ => _.MemberId == request.MemberId, x => x.Comments);
        var memberCases = await caseQuery.ToListAsync(cancellationToken);


        IEnumerable<Comment> comments = memberCases.SelectMany(x => x.Comments);

        var commentDto = comments.Select(_ => new CommentDto(_.Title, _.Text, _.CreatedDate.ToShortDateString(), _.CreatedBy)).ToList();

        var result = memberCases.Select(x => new CaseDto(x.Id, x.Title, x.Description, x.CaseStatus.ToString(), commentDto)).ToList();

        return Result<CaseDto>.Success(values: result);

    }
}
