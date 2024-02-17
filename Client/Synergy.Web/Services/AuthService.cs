using Microsoft.AspNetCore.Authentication;
using Synergy.Shared.Results;
using Synergy.Web.Constraints;
using Synergy.Web.Models.AuthModels;
using System.Security.Claims;

namespace Synergy.Web.Services;

public class AuthService : ClientServiceBase
{
    public AuthService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints, httpContextAccessor)
    {
    }

    public async Task<IResult<LoginOutput>> LoginAsync(LoginInput login)
    {
        HttpResponseMessage httpResponse = await HttpClient.PostAsJsonAsync(Endpoints.Identity.Login, login);

        if (!httpResponse.IsSuccessStatusCode)
        {
            return Result<LoginOutput>.Failure(400);
        }


        LoginOutput? result = await httpResponse.Content.ReadFromJsonAsync<LoginOutput>();

        var authenticationProperties = new AuthenticationProperties();
        authenticationProperties.IsPersistent = login.RememberMe;
        authenticationProperties.ExpiresUtc = result!.TokenExpire;

        var authenticationTokens = new List<AuthenticationToken>
        {
            new AuthenticationToken
            {
                Name = CookieConst.ACCESS_TOKEN,
                Value = result.Token
            },
            new AuthenticationToken
            {
                Name = CookieConst.REFRESH_TOKEN,
                Value = result.RefreshToken
            },
            new AuthenticationToken
            {
                Name = CookieConst.USER_ID,
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
                Name = CookieConst.USER_ROLE,
                Value = result.User.Role!
            });
        }

        var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
        var claimPrinciple = new ClaimsPrincipal(claimsIdentity);

        await HttpContextAccessor.HttpContext!.SignInAsync("Bearer", claimPrinciple, authenticationProperties);
        return Result<LoginOutput>.Success(result);
    }

    public async Task<Shared.Results.IResult> LogoutAsync()
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

    public async Task<Shared.Results.IResult> RegisterAsync(RegisterInput register)
    {
        HttpResponseMessage responseMessage = await HttpClient.PostAsJsonAsync(Endpoints.Identity.Register, register);
        if (responseMessage.IsSuccessStatusCode)
            return Result.Success(204);

        return Result.Failure(400);
    }

    public async Task<IResult<GetUsersOutput>> GetUsersAsync()
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();

        if (hasHeader)
        {
            var responseMessage = await HttpClient.GetAsync(Endpoints.Identity.GetUsers);
            List<GetUsersOutput>? response = await responseMessage.Content.ReadFromJsonAsync<List<GetUsersOutput>>();
            return Result<GetUsersOutput>.Success(response!);
        }

        return Result<GetUsersOutput>.Failure(error: "Server error");
    }

    public async Task<IResult<GetRolesOutput>> GetRolesAsync()
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();
        if (hasHeader)
        {
            List<GetRolesOutput>? responseMessage = await HttpClient.GetFromJsonAsync<List<GetRolesOutput>>(Endpoints.Identity.GetRoles);
            return Result<GetRolesOutput>.Success(values: responseMessage!);
        }

        return Result<GetRolesOutput>.Failure(error: "You must be login!");
    }

    public async Task<Result> UpdateRoleAsync(UpdateRoleInput updateRole)
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();

        if (hasHeader)
        {
            await HttpClient.PutAsJsonAsync(Endpoints.Identity.UpdateRole, updateRole);
            return Result.Success(message: "Role was updated successfully.");
        }

        return Result.Failure(error: "You must be login!");
    }

    public async Task<Result> CreateRoleAsync(CreateRoleInput createRole)
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();
        if (hasHeader)
        {
            await HttpClient.PostAsJsonAsync(Endpoints.Identity.CreateRole, createRole);
            return Result.Success(message: "Role was created successfully.");
        }

        return Result.Failure(error: "You must be login!");
    }

    public async Task<Result> AssignRoleAsync(AssignRoleInput assignRole)
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();

        if (hasHeader)
        {
            var response = await HttpClient.PutAsJsonAsync(Endpoints.Identity.AssignRole, assignRole);

            if (response.IsSuccessStatusCode)
            {
                return Result.Success(message: "Role has been assigned the user.");
            }

            var message = await response.Content.ReadFromJsonAsync<Result>();
            return Result.Failure(error: message!.Message);

        }

        return Result.Failure(error: "You must be login!");
    }

    public async Task<IResult<GetProfileOutput>>GetProfileAsync(string userId)
    {
        GetProfileOutput? response = await HttpClient.GetFromJsonAsync<GetProfileOutput>($"{Endpoints.Identity.GetProfile}/{userId}");
        return Result<GetProfileOutput>.Success(value: response!);
    }

}
