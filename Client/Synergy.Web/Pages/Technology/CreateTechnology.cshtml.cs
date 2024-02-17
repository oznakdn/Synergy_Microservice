using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Models.TechnologyModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Technology;

public class CreateTechnologyModel(TechnologyService technologyService) : PageModel
{
    [BindProperty]
    public CreateTechnologyInput CreateTechnology { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await technologyService.CreateTechnologyAsync(CreateTechnology);

        if (result.IsSuccess)
        {
            return RedirectToPage("/Technology/GetTechnologies");
        }

        return Page();
    }
}
