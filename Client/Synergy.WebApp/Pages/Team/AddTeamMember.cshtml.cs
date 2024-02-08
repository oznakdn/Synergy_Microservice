using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Filters;
using Synergy.WebApp.Models.TeamModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Team;

[ClientAuthenticationFilter]
public class AddTeamMemberModel(TeamService teamService) : PageModel
{


    [BindProperty]
    public AddTeamMemberRequest AddTeamMember { get; set; } = new();

    public IFormFile File { get; set; }
    public void OnGet(string teamId)
    {
        AddTeamMember.TeamId = teamId;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", File.FileName);

        using var stream = new FileStream(path, FileMode.Create);
        await stream.CopyToAsync(stream);


        AddTeamMember.Photo = File.FileName;
        var result = await teamService.AddMemberTeamAsync(AddTeamMember);
        if (result.IsSuccess)
        {
            return RedirectToPage("/Team/GetTeamMembers", new { teamId = AddTeamMember.TeamId });
        }

        return Page();
    }


}
