using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Shared.Results;
using Synergy.WebApp.Helpers;
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

            if (response.IsSuccess)
            {
                CookieHelper.SetCookie(CookieKey.ACCESS_TOKEN, response.Value!.Token, Convert.ToDateTime(response.Value.TokenExpire));

                CookieHelper.SetCookie(CookieKey.REFRESH_TOKEN, response.Value.RefreshToken, Convert.ToDateTime(response.Value.RefreshExpire));

                CookieHelper.SetCookie(CookieKey.USERNAME, response.Value.User.Username, Convert.ToDateTime(response.Value.TokenExpire));

                CookieHelper.SetCookie(CookieKey.EMAIL, response.Value.User.Email, Convert.ToDateTime(response.Value.TokenExpire));

                CookieHelper.SetCookie(CookieKey.ID, response.Value.User.Id, Convert.ToDateTime(response.Value.TokenExpire));

                if (!string.IsNullOrEmpty(response.Value.Role))
                {
                    CookieHelper.SetCookie(CookieKey.ROLE, response.Value.Role, Convert.ToDateTime(response.Value.TokenExpire));
                }


                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
