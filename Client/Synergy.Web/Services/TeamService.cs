using Synergy.Shared.Results;
using Synergy.Web.Models.TeamModels;

namespace Synergy.Web.Services;

public class TeamService : ClientServiceBase
{
    public TeamService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints, httpContextAccessor)
    {
    }

    public async Task<IResult<GetMemberDetailOutput>> GetMemberDetailsAsync(string memberId)
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();
        if (hasHeader)
        {
            var response = await HttpClient.GetAsync($"{Endpoints.Team.GetDeveloperDetails}/{memberId}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<GetMemberDetailOutput>();
                return Result<GetMemberDetailOutput>.Success(value: result!);
            }

            return Result<GetMemberDetailOutput>.Failure();
        }

        return Result<GetMemberDetailOutput>.Failure(error: "You must be login!");
    }

    public async Task<IResult<GetMemberOutput>> GetMembersAsync()
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();
        if (hasHeader)
        {
            var response = await HttpClient.GetAsync(Endpoints.Team.GetDevelopers);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<GetMemberOutput>>();
                return Result<GetMemberOutput>.Success(values: result!);
            }

            return Result<GetMemberOutput>.Failure();
        }

        return Result<GetMemberOutput>.Failure(error: "You must be login!");
    }


    public async Task<Result> AddSkillToMemberAsync(AddSkillToMemberInput addSkillToMember)
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();

        if (hasHeader)
        {
            var response = await HttpClient.PostAsJsonAsync(Endpoints.Team.AddDeveloperSkill, addSkillToMember);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success(message: "The skill has been added to the member.");
            }

            return Result.Failure();
        }

        return Result.Failure(error: "You must be login!");
    }


    public async Task<Result<GetTeamsOutput>> GetTeamsAsync()
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();

        if (hasHeader)
        {
            var responseMessage = await HttpClient.GetAsync(Endpoints.Team.GetTeams);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<List<GetTeamsOutput>>();
                return Result<GetTeamsOutput>.Success(values: result!);
            }

            return Result<GetTeamsOutput>.Failure();
        }

        return Result<GetTeamsOutput>.Failure(error: "You must be login!");

    }

    public async Task<Shared.Results.IResult>CreateTeamAsync(CreateTeamInput createTeam)
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();

        if (hasHeader)
        {
            var response = await HttpClient.PostAsJsonAsync(Endpoints.Team.CreateTeam, createTeam);
            if (response.IsSuccessStatusCode)
            {
                return Result.Success(message: "Team has been created successfully");
            }

            return Result.Failure();
        }

        return Result.Failure(error: "You must be login!");
    }


}
