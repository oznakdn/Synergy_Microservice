using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Synergy.Web.Models.TeamModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Team;

public class AssignMemberModel(TeamService teamService) : PageModel
{

    [BindProperty]
    public AssignMemberInput AssignMember { get; set; } = new();

    public SelectList TeamSelectList { get; set; }

    public async Task OnGetAsync(string memberId)
    {
        AssignMember.MemberId = memberId;

        var teams = await teamService.GetTeamsAsync();
        var teamList = teams.Values!.ToList();

        TeamSelectList = new SelectList(teamList, "Id", "Name");

    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await teamService.AssignMemberAsync(AssignMember);
        if(result.IsSuccess)
        {
            return RedirectToPage("/Auth/GetUsers");
        }

        return Page();

    }
}
