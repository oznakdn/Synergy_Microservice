using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Filters;
using Synergy.WebApp.Models.TeamModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Team;


[ClientAuthenticationFilter]
public class GetTeamMembersModel(TeamService teamService) : PageModel
{

    public string TeamId { get; set; }
    public List<GetDevelopersResponse> Members { get; set; } = new();
    public async Task OnGetAsync(string teamId)
    {
        TeamId = teamId;
        var result = await teamService.GetDevelopersByTeamIdAsync(teamId);
        Members = result.Values!.ToList();
    }
}
