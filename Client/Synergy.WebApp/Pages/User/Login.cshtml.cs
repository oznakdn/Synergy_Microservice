using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Shared.Results;
using Synergy.WebApp.Models.UserModels;
using Synergy.WebApp.Services;
using System.Security.Claims;

namespace Synergy.WebApp.Pages.User
{
    public class LoginModel(UserService userService, IHttpContextAccessor httpContext) : PageModel
    {

        [BindProperty]
        public LoginInput loginInput { get; set; }

        public async Task<IActionResult> OnPost()
        {
            Result<LoginResponse>? response = await userService.LoginAsync(loginInput);

            if (response.IsSuccess)
            {
                httpContext.HttpContext!.Response.Cookies.Append("Access", response.Value!.Token, new CookieOptions
                {
                    Expires = Convert.ToDateTime(response.Value.TokenExpire)
                });

                httpContext.HttpContext!.Response.Cookies.Append("User", response.Value!.User.Username, new CookieOptions
                {
                    Expires = Convert.ToDateTime(response.Value.TokenExpire)
                });

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
