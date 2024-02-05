using Synergy.Shared.Results;
using Synergy.WebApp.Helpers;
using Synergy.WebApp.Models.AuthModels;
using System.Net.Http.Headers;

namespace Synergy.WebApp.Services;

public class AuthService : ClientServiceBase
{
    public AuthService(IHttpClientFactory clientFactory, Endpoints endpoints) : base(clientFactory, endpoints)
    {
    }


    public async Task<Result<GetUsersResponse>> GetUsersAsync()
    {
        string accessToken = CookieHelper.GetCookie(CookieKey.ACCESS_TOKEN);

        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var responseMessage = await HttpClient.GetAsync(Endpoints.Identity.GetUsers);
        if (responseMessage.IsSuccessStatusCode)
        {
            List<GetUsersResponse>? response = await responseMessage.Content.ReadFromJsonAsync<List<GetUsersResponse>>();
            return Result<GetUsersResponse>.Success(response!);
        }

        return Result<GetUsersResponse>.Failure(error: "Server error");

    }
}
