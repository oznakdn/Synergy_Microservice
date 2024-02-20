using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Application.Commands.AssignMemberToTeam;

public class AssignMemberCommandHandler : IRequestHandler<AssignMemberCommand, IResult>
{
    private readonly IRepositoryManager _manager;

    public AssignMemberCommandHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<IResult> Handle(AssignMemberCommand request, CancellationToken cancellationToken)
    {
        var memberQuery = await _manager.Member.GetAsync(_ => _.Id == Guid.Parse(request.AssignMember.MemberId));

        if(!memberQuery.Any())
        {
            return Result.Failure(404,"Member not found!");
        }

        var member = await memberQuery.SingleOrDefaultAsync(cancellationToken);

        var teamQuery = await _manager.Team.GetAsync(_ => _.Id == Guid.Parse(request.AssignMember.TeamId));

        if (!teamQuery.Any())
        {
            return Result.Failure(404, "Team not found!");
        }

        var team = await teamQuery.SingleOrDefaultAsync(cancellationToken);

        member!.TeamId = team!.Id;
        member.ModifiedBy = request.UpdatedBy;
        member.ModifiedDate = DateTime.Now;

        _manager.Member.Update(member);
        var result = await _manager.SaveAsync(cancellationToken);

        if(result == 0)
        {
            return Result.Failure(400);
        }

        return Result.Success(200);



    }
}
