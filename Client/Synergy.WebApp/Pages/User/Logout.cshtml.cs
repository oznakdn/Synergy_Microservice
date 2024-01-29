using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Helpers;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.User;

public class LogoutModel(UserService userService) : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        string refreshToken = CookieHelper.GetCookie(CookieKey.REFRESH_TOKEN);
       
        if(string.IsNullOrEmpty(refreshToken))
        {
            TempData["error"] = "You must be login!";
            return RedirectToPage("/Index");
        }

        var result = await userService.LogoutAsync(refreshToken);

        if (!result.IsSuccess)
        {
            TempData["error"] = result.Message;
            return Page();
        }

        TempData["message"] = result.Message;
        CookieHelper.RemoveCookie(CookieKey.REFRESH_TOKEN);
        CookieHelper.RemoveCookie(CookieKey.ACCESS_TOKEN);
        CookieHelper.RemoveCookie(CookieKey.ID);
        CookieHelper.RemoveCookie(CookieKey.USERNAME);
        CookieHelper.RemoveCookie(CookieKey.EMAIL);
        CookieHelper.RemoveCookie(CookieKey.ROLE);

        return RedirectToPage("/User/Login");
    }
}
