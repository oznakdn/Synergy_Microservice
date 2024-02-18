using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.MemberDtos;

namespace Synergy.TeamService.Application.Queries.GetMembers;

public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, Result<MemberDto>>
{
    private readonly IRepositoryManager _manager;

    public GetMembersQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result<MemberDto>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        var query = await _manager.Member.GetAsync(filter: null
            ,x => x.Skills);

        var developers = await query.ToListAsync();

        var result = developers.Select(x => new MemberDto(x.Id.ToString(), x.GivenName, x.LastName, x.Photo, x.Title)).ToList();

        return Result<MemberDto>.Success(statusCode: 200, values: result);
    }
}
