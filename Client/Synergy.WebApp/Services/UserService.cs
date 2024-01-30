using Synergy.Shared.Results;
using Synergy.WebApp.Models.UserModels;

namespace Synergy.WebApp.Services;

public class UserService : ClientServiceBase
{
    public UserService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints)
    {
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginInput login)
    {
        HttpResponseMessage httpResponse = await HttpClient.PostAsJsonAsync<LoginInput>(Endpoints.Identity.Login, login);

        LoginResponse? result = await httpResponse.Content.ReadFromJsonAsync<LoginResponse>();
        return Result<LoginResponse>.Success(200, result!);
    }

    public async Task<Result> LogoutAsync(string refreshToken)
    {
        HttpResponseMessage httpResponse = await HttpClient.GetAsync($"{Endpoints.Identity.Logout}/{refreshToken}");

        if(httpResponse.IsSuccessStatusCode)
        return Result.Success(200, "Sign out is successfull.");

        return Result.Failure(400,"A Error");
    }

    public async Task<Result>RegisterAsync(RegisterInput register)
    {
        HttpResponseMessage responseMessage = await HttpClient.PostAsJsonAsync<RegisterInput>(Endpoints.Identity.Register, register);
        if (responseMessage.IsSuccessStatusCode)
            return Result.Success(204);

        return Result.Failure(400);
    }

}
