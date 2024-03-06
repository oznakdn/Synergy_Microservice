using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Models.TechnologyModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Technology;

public class CreateTechnologyModel(TechnologyService technologyService, INotyfService notyf) : PageModel
{
    [BindProperty]
    public CreateTechnologyInput CreateTechnology { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await technologyService.CreateTechnologyAsync(CreateTechnology);

        if (result.IsSuccess)
        {
            notyf.Success(result.Message);
            return RedirectToPage("/Technology/GetTechnologies");
        }

        notyf.Error(result.Message);
        return Page();
    }
}
