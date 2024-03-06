using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Filters;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;


[ClientAuthenticationFilter]
public class CreateRoleModel(AuthService authService, INotyfService notyf) : PageModel
{
    [BindProperty]
    public CreateRoleInput CreateRole { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await authService.CreateRoleAsync(CreateRole);
        if (result.IsSuccess)
        {
            notyf.Success(result.Message);
            return RedirectToPage("/Auth/GetRoles");
        }

        notyf.Error(result.Message);
        return Page();
    }
}
