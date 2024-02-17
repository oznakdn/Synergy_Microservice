using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;

public class LogoutModel(AuthService authService) : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {

        var result = await authService.LogoutAsync();

        if (!result.IsSuccess)
        {
            ViewData["LogoutFailure"] = result.Message;
            return Page();
        }

        TempData["LogoutSuccess"] = result.Message;
        return RedirectToPage("/Auth/Login");
    }
}
