using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.ProjectService.Domain.Models;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;
using Synergy.ProjectService.Shared.Dtos.CommentDtos;
using Synergy.ProjectService.Shared.Dtos.IssueDtos;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Queries.GetIssuesByMemberId;

public class GetIssuesByMemberIdQueryHandler : IRequestHandler<GetIssuesByMemberIdQuery, IResult<IssueDto>>
{
    private readonly IRepositoryManager _manager;

    public GetIssuesByMemberIdQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<IResult<IssueDto>> Handle(GetIssuesByMemberIdQuery request, CancellationToken cancellationToken)
    {
        var issueQuery = await _manager.Issue.GetAsync(_ => _.MemberId == request.MemberId, x => x.Comments);
        var memberCases = await issueQuery.ToListAsync(cancellationToken);


        IEnumerable<Comment> comments = memberCases.SelectMany(x => x.Comments);

        var commentDto = comments.Select(_ => new CommentDto(_.Text, _.CreatedDate.ToShortDateString(), _.CreatedBy)).ToList();

        var result = memberCases.Select(x => new IssueDto(x.Id, x.Summary, x.Description, commentDto)).ToList();

        return Result<IssueDto>.Success(values: result);

    }
}
