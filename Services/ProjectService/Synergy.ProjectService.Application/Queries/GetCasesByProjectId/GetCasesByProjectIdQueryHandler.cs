using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;
using Synergy.ProjectService.Shared.Dtos.CaseDtos;
using Synergy.ProjectService.Shared.Dtos.CommentDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Queries.GetCasesByProjectId;

public class GetCasesByProjectIdQueryHandler : IRequestHandler<GetCasesByProjectIdQuery, IResult<CaseDto>>
{
    private readonly IRepositoryManager _manager;

    public GetCasesByProjectIdQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<IResult<CaseDto>> Handle(GetCasesByProjectIdQuery request, CancellationToken cancellationToken)
    {
        var caseQuery = await _manager.Case.GetAsync(_ => _.ProjectId == request.ProjectId, x => x.Comments);

        var cases =await caseQuery.ToListAsync(cancellationToken);

        var comments = cases.SelectMany(x => x.Comments);

        var commentDto = comments.Select(_ => new CommentDto(_.Title, _.Text, _.CreatedDate.ToShortDateString(), _.CreatedBy)).ToList();

        var result = cases.Select(_ => new CaseDto(_.Id, _.Title, _.Description, _.CaseStatus.ToString(), commentDto)).ToList();

        return Result<CaseDto>.Success(values: result);
    }
}
