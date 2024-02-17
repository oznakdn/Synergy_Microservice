using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;

public class RegisterModel(AuthService authService) : PageModel
{
    [BindProperty]
    public RegisterInput RegisterInput { get; set; }

    public async Task<IActionResult> OnPost()
    {
        var response = await authService.RegisterAsync(RegisterInput);
        if (!response.IsSuccess)
        {
            ViewData["RegisterFailure"] = "Register could not be!";
            return Page();
        }

        TempData["RegisterSuccess"] = "Registered was be success.";
        return RedirectToPage("/Auth/Login", new { Username = RegisterInput.Username });
    }
}
