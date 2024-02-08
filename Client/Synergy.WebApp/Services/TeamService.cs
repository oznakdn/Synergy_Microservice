
using Synergy.Shared.Results;
using Synergy.WebApp.Models.TeamModels;

namespace Synergy.WebApp.Services;

public class TeamService : ClientServiceBase
{
    public TeamService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints, httpContextAccessor)
    {
    }

    public async Task<Result<GetTeamsResponse>> GetTeamsAsync()
    {
        var hasHeader = await base.AddAuthorizeHeader();

        if (hasHeader)
        {
            var responseMessage = await HttpClient.GetAsync(Endpoints.Team.GetTeams);

            if(responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<List<GetTeamsResponse>>();
                return Result<GetTeamsResponse>.Success(values: result!);
            }

            return Result<GetTeamsResponse>.Failure();
        }

        return Result<GetTeamsResponse>.Failure(error: "You must be login!");

    }

    public async Task<Result<GetTeamDeveloper>> GetDevelopersByTeamIdAsync(string teamId)
    {
        var hasHeader = await base.AddAuthorizeHeader();
        if (hasHeader)
        {
            var responseMessage = await HttpClient.GetAsync($"{Endpoints.Team.GetDevelopersByTeamId}/{teamId}");

            if(responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<List<GetTeamDeveloper>>();
                return Result<GetTeamDeveloper>.Success(values: result!);
            }

            return Result<GetTeamDeveloper>.Failure();
        }

        return Result<GetTeamDeveloper>.Failure(error: "You must be login!");

    }

    public async Task<Result> AddMemberTeamAsync(AddTeamMemberRequest addTeamMember)
    {
        var hasHeader = await base.AddAuthorizeHeader();

        if (hasHeader)
        {
            var response = await HttpClient.PostAsJsonAsync<AddTeamMemberRequest>(Endpoints.Team.CreateDeveloper, addTeamMember);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success(message: "The member has been added to the team.");
            }

            return Result.Failure();
        }

        return Result.Failure(error: "You must be login!");

    }


}
