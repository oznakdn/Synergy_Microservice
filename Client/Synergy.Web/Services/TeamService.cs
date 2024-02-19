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
}
