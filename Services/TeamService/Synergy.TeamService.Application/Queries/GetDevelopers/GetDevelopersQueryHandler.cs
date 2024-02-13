using MediatR;
using Microsoft.EntityFrameworkCore;
using Synergy.Shared.Results;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using Synergy.TeamService.Shared.Dtos.DeveloperDtos;
using System.Linq;

namespace Synergy.TeamService.Application.Queries.GetDevelopers;

public class GetDevelopersQueryHandler : IRequestHandler<GetDevelopersQuery, Result<DeveloperDto>>
{
    private readonly IRepositoryManager _manager;

    public GetDevelopersQueryHandler(IRepositoryManager manager)
    {
        _manager = manager;
    }

    public async Task<Result<DeveloperDto>> Handle(GetDevelopersQuery request, CancellationToken cancellationToken)
    {
        var query = await _manager.Developer.GetAsync(filter: null
            , x => x.Team!, x => x.Skills);

        var developers = await query.ToListAsync();

        var result = developers.Select(x => new DeveloperDto(x.Id.ToString(), x.GivenName, x.LastName, x.Photo, x.Title, x.Team.TeamName)).ToList();

        return Result<DeveloperDto>.Success(statusCode: 200, values: result);
    }
}
