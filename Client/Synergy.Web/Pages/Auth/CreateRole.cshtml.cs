using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Filters;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;


[ClientAuthenticationFilter]
public class CreateRoleModel(AuthService authService) : PageModel
{
    [BindProperty]
    public CreateRoleInput CreateRole { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await authService.CreateRoleAsync(CreateRole);
        if (result.IsSuccess)
        {
            TempData["CreateRoleSuccess"] = result.Message;
            return RedirectToPage("/Auth/GetRoles");
        }

        ViewData["CreateRoleFailure"] = result.Message;
        return Page();
    }
}
