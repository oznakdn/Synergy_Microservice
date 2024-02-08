using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.TeamDtos;

namespace Synergy.TeamService.Application.Queries.GetDevelopersByTeamId;

internal class GetDevelopersByTeamIdQueryHandler : IRequestHandler<GetDevelopersByTeamIdQuery, Result<TeamDevelopers>>
{
    private readonly IDeveloperRepo _developerRepo;
    public GetDevelopersByTeamIdQueryHandler(IDeveloperRepo developerRepo)
    {
        _developerRepo = developerRepo;
    }

    public async Task<Result<TeamDevelopers>> Handle(GetDevelopersByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var query = await _developerRepo.GetAsync(_ => _.TeamId == Guid.Parse(request.TeamId));
        var developers = await query.ToListAsync(cancellationToken);

        var developersDto = developers.Select(d => new TeamDevelopers(d.GivenName, d.LastName, d.Title,d.Photo)).ToList();
        return Result<TeamDevelopers>.Success(values: developersDto);
    }
}
