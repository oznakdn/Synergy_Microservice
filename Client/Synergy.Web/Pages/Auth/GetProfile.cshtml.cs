using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;

public class GetProfileModel(AuthService authService) : PageModel
{
    public GetProfileOutput Profile { get; set; }
    public async Task OnGet(string userId)
    {
        var result = await authService.GetProfileAsync(userId);
        Profile = result.Value!;
    }
}
