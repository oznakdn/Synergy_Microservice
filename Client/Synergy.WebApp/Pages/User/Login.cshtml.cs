using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Shared.Results;
using Synergy.WebApp.Models.UserModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.User
{
    public class LoginModel(UserService userService) : PageModel
    {

        [BindProperty]
        public LoginInput loginInput { get; set; }

        public async Task<IActionResult> OnPost()
        {
            Result<LoginResponse>? response = await userService.LoginAsync(loginInput);

            if(response.IsSuccess)
            {
                TempData["loginSuccess"] = "You are in the system!";
                return RedirectToPage("/Index");
            }

            ViewData["loginError"] = "Username or password is wrong!";

            return Page();
        }
    }
}
