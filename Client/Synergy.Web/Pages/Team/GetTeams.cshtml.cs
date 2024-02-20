using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Constraints;
using Synergy.Web.Models.TeamModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Team;

public class GetTeamsModel(TeamService teamService) : PageModel
{
    public List<GetTeamsOutput> Teams { get; set; } = new();

    [BindProperty]
    public CreateTeamInput CreateInput { get; set; }

    [BindProperty]
    public UpdateTeamInput UpdateInput { get; set; }

    [BindProperty]
    public string? AccessToken { get; set; }

    public async Task OnGetAsync()
    {
        AccessToken = await HttpContext!.GetTokenAsync(CookieConst.ACCESS_TOKEN);
        var result = await teamService.GetTeamsAsync();
        Teams = result.Values!.ToList();
    }

}
