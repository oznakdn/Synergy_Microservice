using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Synergy.Web.Constraints;
using Synergy.Web.Models.AuthModels;
using System.Security.Claims;

namespace Synergy.Web.Filters;

public class ClientAuthenticationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
{

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var clientService = context.HttpContext.RequestServices.GetRequiredService<HttpClient>();

        string? accessToken = await context.HttpContext.GetTokenAsync(CookieConst.ACCESS_TOKEN);

        if (string.IsNullOrEmpty(accessToken))
        {

            string? refreshToken = await context.HttpContext.GetTokenAsync(CookieConst.REFRESH_TOKEN);

            if (!string.IsNullOrEmpty(refreshToken))
            {
                string url = $"http://localhost:5000/services/identity/users/relogin/{refreshToken}";

                var response = await clientService.GetFromJsonAsync<LoginOutput>(url);

                var authenticationProperties = new AuthenticationProperties();
                authenticationProperties.ExpiresUtc = response!.TokenExpire;

                var authenticationTokens = new List<AuthenticationToken>
                {
                    new AuthenticationToken
                    {
                        Name = CookieConst.ACCESS_TOKEN,
                        Value = response.Token
                    },
                    new AuthenticationToken
                    {
                        Name = CookieConst.REFRESH_TOKEN,
                        Value = response.RefreshToken
                    },
                    new AuthenticationToken
                    {
                         Name = CookieConst.USER_ID,
                         Value = response.User.Id
                    }
                };

                authenticationProperties.StoreTokens(authenticationTokens);
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name,response.User.Username),
                     new Claim(ClaimTypes.Email,response.User.Email),
                     new Claim(ClaimTypes.NameIdentifier,response.User.Id)
                };

                if (!string.IsNullOrEmpty(response.User.Role))
                {
                    claims.Add(new Claim(ClaimTypes.Role, response.User.Role!));
                    authenticationTokens.Add(new AuthenticationToken
                    {
                        Name = CookieConst.USER_ROLE,
                        Value = response.User.Role!
                    });
                }

                var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
                var claimPrinciple = new ClaimsPrincipal(claimsIdentity);

                await context.HttpContext!.SignInAsync("Bearer", claimPrinciple, authenticationProperties);

                context.Result = new RedirectToPageResult("/Index");

            }

            context.Result = new RedirectToPageResult("/Login");

        }


    }
}
