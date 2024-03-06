using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;

public class LogoutModel(AuthService authService, INotyfService notyf) : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {

        var result = await authService.LogoutAsync();

        if (!result.IsSuccess)
        {
            notyf.Error(result.Message);
            return Page();
        }

        notyf.Success(result.Message);
        return RedirectToPage("/Auth/Login");
    }
}
