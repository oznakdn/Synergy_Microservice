using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.User;

public class LogoutModel(UserService userService) : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        
        var result = await userService.LogoutAsync();

        if (!result.IsSuccess)
        {
            ViewData["error"] = result.Message;
            return Page();
        }

        ViewData["message"] = result.Message;
        return RedirectToPage("/User/Login");
    }
}
