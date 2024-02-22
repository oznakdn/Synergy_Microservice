using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Synergy.Web.Filters;
using Synergy.Web.Models.ProjectModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Project;


[ClientAuthenticationFilter]
public class GetProjectsModel(ProjectService projectService, TeamService teamService) : PageModel
{
    public List<GetProjectsOutput> Projects { get; set; } = new();

    [BindProperty]
    public CreateProjectInput CreateProject { get; set; }

    public SelectList TeamSelectList { get; set; }

    public string TeamName { get; set; }

    public async Task OnGetAsync()
    {
        var teams = await teamService.GetTeamsAsync();
        TeamSelectList = new SelectList(teams.Values!.ToList(), "Id", "Name");

        var result = await projectService.GetProjectsAsync();
        if(result.IsSuccess)
        {
            Projects = result.Values!.ToList();
        }

    }

    public async Task<IActionResult>OnPostAsync()
    {
        var teams = await teamService.GetTeamsAsync();
        TeamSelectList = new SelectList(teams.Values!.ToList(), "Id", "Name");
        if (ModelState.IsValid)
        {
            var result = await projectService.CreateprojectAsync(CreateProject);

            if (result.IsSuccess)
            {
                return RedirectToPage("/Project/GetProjects");
            }

            return Page();
        }

        return Page();
    }
}
