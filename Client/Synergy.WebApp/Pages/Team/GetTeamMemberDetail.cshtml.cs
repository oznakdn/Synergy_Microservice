using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Models.TeamModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Team;

public class GetTeamMemberDetailModel(TeamService teamService) : PageModel
{
    public GetDeveloperDetailsResponse? Member { get; set; }
    public async Task OnGetAsync(string memberId)
    {
        var result = await teamService.GetDeveloperDetails(memberId);
        Member = result.Value;
    }
}
