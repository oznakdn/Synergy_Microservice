using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Synergy.WebApp.Models.TeamModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Team;

public class AddSkillToMemberModel(TeamService teamService, TechnologyService technologyService) : PageModel
{
   
    [BindProperty]
    public AddSkillToMemberRequest AddSkillToMember { get; set; } = new();

    public SelectList TechnologiesList { get; set; }
    public SelectList ExperienceEnumList { get; set; }

    public async Task OnGetAsync(string developerId)
    {
        var technologies = await technologyService.GetTechnologiesAsync();
        AddSkillToMember.DeveloperId = developerId;
        TechnologiesList = new SelectList(technologies.Values, "Id", "Name");
        ExperienceEnumList = new SelectList(new List<string>
        {
            "0-1 year",
            "1-3 year",
            "3-5 year",
            "5- year",
        });
    }

    public async Task<IActionResult>OnPostAsync()
    {
        var response = await teamService.AddSkillToMemberAsync(AddSkillToMember);
        if(response.IsSuccess)
        {
            return RedirectToPage("/Team/GetTeamMemberDetail", new { memberId  = AddSkillToMember.DeveloperId});
        }

        return Page();
    }
}
