using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Filters;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;


[ClientAuthenticationFilter]
public class GetRolesModel(AuthService authService) : PageModel
{
    public List<GetRolesOutput> Roles { get; set; } = new();
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
