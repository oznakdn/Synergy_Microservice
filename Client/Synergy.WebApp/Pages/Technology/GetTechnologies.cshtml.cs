using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Filters;
using Synergy.WebApp.Models.TechnologyModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Technology;

[ClientAuthenticationFilter]
public class GetTechnologiesModel(TechnologyService technologyService) : PageModel
{

    public List<GetTechnologiesResponse> Technologies { get; set; } = new();
    public async Task OnGetAsync()
    {
        var result = await technologyService.GetTechnologiesAsync();
        Technologies = result.Values.ToList();
    }
}
