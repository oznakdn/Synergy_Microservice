using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Models.TeamModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Team;

public class GetTeamsModel(TeamService teamService) : PageModel
{
    public List<GetTeamsResponse> Teams { get; set; } = new();

    public async Task OnGet()
    {
        var response = await teamService.GetTeamsAsync();
        Teams = response.Values!.ToList();
    }
}
