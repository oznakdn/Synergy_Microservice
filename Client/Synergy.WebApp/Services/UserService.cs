using Synergy.Shared.Results;
using Synergy.WebApp.Models.UserModels;

namespace Synergy.WebApp.Services;

public class UserService : ClientServiceBase
{
    public UserService(IHttpClientFactory clientFactory, Endpoints endpoints) : base(clientFactory, endpoints)
    {
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginInput login)
    {
        HttpResponseMessage httpResponse = await HttpClient.PostAsJsonAsync<LoginInput>(Endpoints.Identity.Login, login);
      
            LoginResponse? result = await httpResponse.Content.ReadFromJsonAsync<LoginResponse>();
            return Result<LoginResponse>.Success(200, result!);
    }

}
