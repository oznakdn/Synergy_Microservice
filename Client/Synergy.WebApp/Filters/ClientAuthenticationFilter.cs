using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Synergy.WebApp.Constants;
using Synergy.WebApp.Models.UserModels;
using System.Security.Claims;

namespace Synergy.WebApp.Filters;

public class ClientAuthenticationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
{

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var clientService = context.HttpContext.RequestServices.GetRequiredService<HttpClient>();

        string? accessToken = await context.HttpContext.GetTokenAsync("access_token");

        if (string.IsNullOrEmpty(accessToken))
        {

            string? refreshToken = await context.HttpContext.GetTokenAsync("refresh_token");

            if (!string.IsNullOrEmpty(refreshToken))
            {
                string url = $"http://localhost:5000/services/identity/users/relogin/{refreshToken}";

                var response = await clientService.GetFromJsonAsync<LoginResponse>(url);

                var authenticationProperties = new AuthenticationProperties();
                authenticationProperties.ExpiresUtc = response!.TokenExpire;

                var authenticationTokens = new List<AuthenticationToken>
                {
                    new AuthenticationToken
                    {
                        Name = TokenConsts.ACCESS_TOKEN,
                        Value = response.Token
                    },
                    new AuthenticationToken
                    {
                        Name = TokenConsts.REFRESH_TOKEN,
                        Value = response.RefreshToken
                    },
                    new AuthenticationToken
                    {
                         Name = TokenConsts.USER_ID,
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
                        Name = TokenConsts.USER_ROLE,
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
