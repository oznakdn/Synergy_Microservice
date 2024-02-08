
using Synergy.Shared.Results;
using Synergy.WebApp.Models.TeamModels;

namespace Synergy.WebApp.Services;

public class TeamService : ClientServiceBase
{
    public TeamService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints, httpContextAccessor)
    {
    }

    public async Task<Result<GetTeamsResponse>>GetTeamsAsync()
    {
        var hasHeader = await base.AddAuthorizeHeader();

        if(hasHeader)
        {
            var responseMessage = await HttpClient.GetAsync(Endpoints.Team.GetTeams);
            var result = await responseMessage.Content.ReadFromJsonAsync<List<GetTeamsResponse>>();
            return Result<GetTeamsResponse>.Success(values: result!);
        }

        return Result<GetTeamsResponse>.Failure(error: "You must be login!");

    }

    public async Task<Result<GetTeamDeveloper>>GetDevelopersByTeamIdAsync(string teamId)
    {
        var hasHeader = await base.AddAuthorizeHeader();
        if(hasHeader)
        {
            var responseMessage = await HttpClient.GetAsync($"{Endpoints.Team.GetDevelopersByTeamId}/{teamId}");
            var result = await responseMessage.Content.ReadFromJsonAsync<List<GetTeamDeveloper>>();
            return Result<GetTeamDeveloper>.Success(values: result!);
        }

        return Result<GetTeamDeveloper>.Failure(error: "You must be login!");

    }


}
