using MediatR;
using Synergy.ProjectService.Domain.Models.Enums;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Commands.CreateIssue;

public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, IResult>
{
    private readonly IRepositoryManager _manager;

    public CreateIssueCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<IResult> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        _manager.Issue.Insert(new Domain.Models.Issue
        {
            StatusId = request.CreateIssue.StatusId,
            Summary = request.CreateIssue.Summary,
            MemberId = request.CreateIssue.MemberId,
            PriorityType = (PriorityType)request.CreateIssue.PriorityType,
            IssueType = (IssueType)request.CreateIssue.IssueType,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = request.CreatedBy,
            StartDate = request.CreateIssue.StartDate,
            EndDate = request.CreateIssue.EndDate
        });

        var result = await _manager.SaveAsync(cancellationToken);
        if(result == 0)
        {
            return Result.Failure(400);
        }

        return Result.Success(204);
    }
}
