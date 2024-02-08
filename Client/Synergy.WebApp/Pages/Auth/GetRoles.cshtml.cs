using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Filters;
using Synergy.WebApp.Models.AuthModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Auth;


[ClientAuthenticationFilter]
public class GetRolesModel(AuthService authService) : PageModel
{
    public List<GetRolesResponse> Roles { get; set; } = new();
    public async Task<IActionResult> OnGet()
    {
        var result = await authService.GetRolesAsync();
        if (result.IsSuccess)
        {
            Roles = result.Values!.ToList();
            return Page();
        }

        return RedirectToPage("/Auth/Login");
    }
}
