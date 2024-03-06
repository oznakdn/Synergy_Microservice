using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Synergy.Web.Models.TeamModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Team;

public class AddSkillToMemberModel(TeamService teamService, TechnologyService technologyService, INotyfService notyf) : PageModel
{
    [BindProperty]
    public AddSkillToMemberInput AddSkillToMember { get; set; } = new();

    public SelectList TechnologiesList { get; set; }
    public SelectList ExperienceEnumList { get; set; }

    public async Task OnGetAsync(string memberId)
    {
        var technologies = await technologyService.GetTechnologiesAsync();
        AddSkillToMember.DeveloperId = memberId;
        TechnologiesList = new SelectList(technologies.Values, "Id", "Name");
        ExperienceEnumList = new SelectList(new List<string>
        {
            "0-1 year",
            "1-3 year",
            "3-5 year",
            "5- year",
        });
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await teamService.AddSkillToMemberAsync(AddSkillToMember);
        if (response.IsSuccess)
        {
            notyf.Success(response.Message);
            return RedirectToPage("/Team/GetMemberDetails", new { memberId = AddSkillToMember.DeveloperId });
        }

        notyf.Error(response.Message);
        return Page();
    }
}
