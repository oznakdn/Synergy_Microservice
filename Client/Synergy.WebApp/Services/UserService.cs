using Microsoft.AspNetCore.Authentication;
using Synergy.Shared.Results;
using Synergy.WebApp.Models.UserModels;
using System.Security.Claims;

namespace Synergy.WebApp.Services;



public class UserService : ClientServiceBase
{
    public UserService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints, httpContextAccessor)
    {
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginInput login)
    {
        HttpResponseMessage httpResponse = await HttpClient.PostAsJsonAsync<LoginInput>(Endpoints.Identity.Login, login);

        if (!httpResponse.IsSuccessStatusCode)
        {
            return (Result<LoginResponse>)Result<LoginResponse>.Failure(400);
        }


        LoginResponse? result = await httpResponse.Content.ReadFromJsonAsync<LoginResponse>();

        var authenticationProperties = new AuthenticationProperties();
        authenticationProperties.IsPersistent = login.RememberMe;
        authenticationProperties.ExpiresUtc = Convert.ToDateTime(result!.TokenExpire);

        var authenticationTokens = new List<AuthenticationToken>
        {
            new AuthenticationToken
            {
                Name = "access_token",
                Value = result.Token
            },
            new AuthenticationToken
            {
                Name = "refresh_token",
                Value = result.RefreshToken
            },
            new AuthenticationToken
            {
                Name = "id",
                Value = result.User.Id
            }
        };

        authenticationProperties.StoreTokens(authenticationTokens);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,result.User.Username),
            new Claim(ClaimTypes.Email,result.User.Email),
            new Claim(ClaimTypes.NameIdentifier,result.User.Id)
        };



        if (!string.IsNullOrEmpty(result.User.Role))
        {
            claims.Add(new Claim(ClaimTypes.Role, result.User.Role!));
            authenticationTokens.Add(new AuthenticationToken
            {
                Name = "role",
                Value = result.User.Role!
            });
        }

        var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
        var claimPrinciple = new ClaimsPrincipal(claimsIdentity);

        await HttpContextAccessor.HttpContext!.SignInAsync("Bearer", claimPrinciple, authenticationProperties);
        return Result<LoginResponse>.Success(result);
    }

    public async Task<Result> LogoutAsync()
    {
        string? refreshToken = await HttpContextAccessor.HttpContext!.GetTokenAsync("refresh_token");

        if (!string.IsNullOrEmpty(refreshToken))
        {
            HttpResponseMessage httpResponse = await HttpClient.GetAsync($"{Endpoints.Identity.Logout}/{refreshToken}");
            if (httpResponse.IsSuccessStatusCode)
            {
                await HttpContextAccessor.HttpContext!.SignOutAsync("Bearer");
                return Result.Success(200, "Sign out is successfull.");
            }
        }

        return Result.Failure(400, "A Error");
    }

    public async Task<Result> RegisterAsync(RegisterInput register)
    {
        HttpResponseMessage responseMessage = await HttpClient.PostAsJsonAsync<RegisterInput>(Endpoints.Identity.Register, register);
        if (responseMessage.IsSuccessStatusCode)
            return Result.Success(204);

        return Result.Failure(400);
    }

}
