using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.MemberDtos;

namespace Synergy.TeamService.Application.Queries.GetMembersByTeamId;

internal class GetMembersByTeamIdQueryHandler : IRequestHandler<GetMembersByTeamIdQuery, Result<MemberDto>>
{
    private readonly IMemberRepo _developerRepo;
    public GetMembersByTeamIdQueryHandler(IMemberRepo developerRepo)
    {
        _developerRepo = developerRepo;
    }

    public async Task<Result<MemberDto>> Handle(GetMembersByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var query = await _developerRepo.GetAsync(filter: _ => _.TeamId == Guid.Parse(request.TeamId), x => x.Team);
        var developers = await query.ToListAsync(cancellationToken);

        var developer = developers.Select(x => new MemberDto(x.Id.ToString(), x.GivenName, x.LastName, x.Photo, x.Title, x.Team!.TeamName)).ToList();

        return Result<MemberDto>.Success(values: developer);
    }
}
