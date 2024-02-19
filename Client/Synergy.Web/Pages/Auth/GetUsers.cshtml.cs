using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Filters;
using Synergy.Web.Models.TeamModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;


[ClientAuthenticationFilter]
public class GetUsersModel(TeamService teamService) : PageModel
{
    public List<GetMemberOutput> Members { get; set; } = new();
    public async Task OnGetAsync()
    {
        var result = await teamService.GetMembersAsync();
        Members = result.Values!.ToList();
    }
}
