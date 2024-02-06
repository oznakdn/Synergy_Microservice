using Synergy.Shared.Results;
using Synergy.WebApp.Models.AuthModels;

namespace Synergy.WebApp.Services;

public class AuthService : ClientServiceBase
{
    public AuthService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints, httpContextAccessor)
    {
    }

    public async Task<Result<GetUsersResponse>> GetUsersAsync()
    {
        await base.AddAuthorizeHeader();
        var responseMessage = await HttpClient.GetAsync(Endpoints.Identity.GetUsers);
        if (responseMessage.IsSuccessStatusCode)
        {
            List<GetUsersResponse>? response = await responseMessage.Content.ReadFromJsonAsync<List<GetUsersResponse>>();
            return Result<GetUsersResponse>.Success(response!);
        }

        return Result<GetUsersResponse>.Failure(error: "Server error");

    }
}
