using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.DeveloperDtos;

namespace Synergy.TeamService.Application.Queries.GetDevelopersByTeamId;

internal class GetDevelopersByTeamIdQueryHandler : IRequestHandler<GetDevelopersByTeamIdQuery, Result<DeveloperDto>>
{
    private readonly IMemberRepo _developerRepo;
    public GetDevelopersByTeamIdQueryHandler(IMemberRepo developerRepo)
    {
        _developerRepo = developerRepo;
    }

    public async Task<Result<DeveloperDto>> Handle(GetDevelopersByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var query = await _developerRepo.GetAsync(filter: _ => _.TeamId == Guid.Parse(request.TeamId), x => x.Team);
        var developers = await query.ToListAsync(cancellationToken);

        var developer = developers.Select(x => new DeveloperDto(x.Id.ToString(), x.GivenName, x.LastName, x.Photo, x.Title, x.Team!.TeamName)).ToList();

        return Result<DeveloperDto>.Success(values: developer);
    }
}
