using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
                authenticationProperties.ExpiresUtc = Convert.ToDateTime(response!.TokenExpire);

                var authenticationTokens = new List<AuthenticationToken>
                {
                    new AuthenticationToken
                    {
                        Name = "access_token",
                        Value = response.Token
                    },
                    new AuthenticationToken
                    {
                        Name = "refresh_token",
                        Value = response.RefreshToken
                    }
                };

                authenticationProperties.StoreTokens(authenticationTokens);
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Name,response.User.Username),
                     new Claim(ClaimTypes.Email,response.User.Email),
                     new Claim(ClaimTypes.NameIdentifier,response.User.Id)
                };



                if (string.IsNullOrEmpty(response.User.Role))
                {
                    claims.Add(new Claim(ClaimTypes.Role, response.User.Role!));
                    authenticationTokens.Add(new AuthenticationToken
                    {
                        Name = "role",
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
