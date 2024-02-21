using MediatR;
using Synergy.ProjectService.Domain.Models.Enums;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;
using Synergy.Shared.Results;

namespace Synergy.ProjectService.Application.Commands.CreateCase;

public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, IResult>
{
    private readonly IRepositoryManager _manager;

    public CreateCaseCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<IResult> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        _manager.Case.Insert(new Domain.Models.Case
        {
            ProjectId = request.CreateCase.ProjectId,
            Title = request.CreateCase.Title,
            MemberId = request.CreateCase.MemberId,
            CaseStatus = (Status)request.CreateCase.CaseStatus,
            CreatedDate = DateTime.UtcNow,
            CreatedBy = request.CreatedBy,
            StartDate = request.CreateCase.StartDate,
            EndDate = request.CreateCase.EndDate
        });

        var result = await _manager.SaveAsync(cancellationToken);
        if(result == 0)
        {
            return Result.Failure(400);
        }

        return Result.Success(204);
    }
}
