using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Models.TeamModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Team;

public class GetMemberDetailsModel(TeamService teamService) : PageModel
{
    public GetMemberDetailOutput GetMemberDetail { get; set; }
    public async Task OnGetAsync(string memberId)
    {
        var result = await teamService.GetMemberDetailsAsync(memberId);

        if(result.IsSuccess)
        {
            GetMemberDetail = result.Value!;
        }
    }
}
