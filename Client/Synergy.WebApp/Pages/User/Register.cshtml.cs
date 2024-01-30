using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Models.UserModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.User;

public class RegisterModel(UserService userService) : PageModel
{
    [BindProperty]
    public RegisterInput RegisterInput { get; set; }

    public async Task<IActionResult> OnPost()
    {
        var response = await userService.RegisterAsync(RegisterInput);
        if (!response.IsSuccess)
        {
            ViewData["error"] = "Register could not be!";
            return Page();
        }

        ViewData["success"] = "Registered was be success.";
        return RedirectToPage("/User/Login", new { Username = RegisterInput.Username });
    }
}
