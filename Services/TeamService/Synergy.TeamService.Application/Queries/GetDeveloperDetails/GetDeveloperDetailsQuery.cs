using MediatR;
using Synergy.Shared.Results;
using Synergy.TeamService.Shared.Dtos.DeveloperDtos;

namespace Synergy.TeamService.Application.Queries.GetDeveloperDetails;

public class GetDeveloperDetailsQuery : IRequest<IResult<DeveloperDetailsDto>>
{
    public GetDeveloperDetailsQuery(string developerId)
    {
        DeveloperId = developerId;
    }

    public string DeveloperId { get; set; }
}
