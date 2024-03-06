using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Shared.Results;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;

public class LoginModel(AuthService authService , INotyfService notyf) : PageModel
{

    [BindProperty]
    public LoginInput LoginInput { get; set; }

    public async Task<IActionResult> OnPost()
    {
        IResult<LoginOutput>? response = await authService.LoginAsync(LoginInput);

        if (response.IsSuccess)
        {
            TempData["Success"] = "Welcome to SYNERGY";
            notyf.Success("Welcome to SYNERGY");
            return RedirectToPage("/Index");
        }

        notyf.Error("Username or password is wrong!");

        return Page();
    }
}
