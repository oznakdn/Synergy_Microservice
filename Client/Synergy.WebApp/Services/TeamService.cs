
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
        await base.AddAuthorizeHeader();
        var responseMessage = await HttpClient.GetAsync(Endpoints.Team.GetTeams);

        var result = await responseMessage.Content.ReadFromJsonAsync<List<GetTeamsResponse>>();
        return Result<GetTeamsResponse>.Success(values: result!);
    }

    public async Task<Result<GetTeamDeveloper>>GetDevelopersByTeamIdAsync(string teamId)
    {
        await base.AddAuthorizeHeader();
        var responseMessage = await HttpClient.GetAsync($"{Endpoints.Team.GetDevelopersByTeamId}/{teamId}");
        var result = await responseMessage.Content.ReadFromJsonAsync<List<GetTeamDeveloper>>();
        return Result<GetTeamDeveloper>.Success(values: result!);
    }


}
