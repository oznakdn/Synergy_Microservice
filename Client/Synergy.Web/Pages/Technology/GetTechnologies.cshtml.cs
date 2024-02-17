using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Filters;
using Synergy.Web.Models.TechnologyModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Technology;

[ClientAuthenticationFilter]
public class GetTechnologiesModel(TechnologyService technologyService) : PageModel
{
    public List<GetTechnologiesOutput> Technologies { get; set; } = new();
    public async Task OnGetAsync()
    {
        var result = await technologyService.GetTechnologiesAsync();
        Technologies = result.Values.ToList();
    }
}
