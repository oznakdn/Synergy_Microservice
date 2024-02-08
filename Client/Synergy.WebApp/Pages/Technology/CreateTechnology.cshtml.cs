using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Filters;
using Synergy.WebApp.Models.TechnologyModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Technology;


[ClientAuthenticationFilter]
public class CreateTechnologyModel(TechnologyService technologyService) : PageModel
{

    [BindProperty]
    public CreateTechnologyRequest CreateTechnology { get; set; }

    public async Task<IActionResult>OnPostAsync()
    {
        var result = await technologyService.CreateTechnologyAsync(CreateTechnology);

        if(result.IsSuccess)
        {
            return RedirectToPage("/Technology/GetTechnologies");
        }

        return Page();
    }
}
