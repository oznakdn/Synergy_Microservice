using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Models.TeamModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Team;

public class GetMemberDetailsModel(TeamService teamService) : PageModel
{
    public GetMemberDetailOutput GetMemberDetail { get; set; }
    public string? TeamName { get; set; }
    public async Task OnGetAsync(string memberId)
    {
        var memberResult = await teamService.GetMemberDetailsAsync(memberId);
        var teamsResult = await teamService.GetTeamsAsync();

        var member = memberResult.Value;
        var teams = teamsResult.Values;

        var existMemberTeam =  teams!.SingleOrDefault(x => x.Id == member!.Member.TeamId);
        if(existMemberTeam is not null)
        {
            TeamName = existMemberTeam.Name;
        }

        if (memberResult.IsSuccess)
        {
            GetMemberDetail = member!;
        }
    }
}
