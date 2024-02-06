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

    public async Task<Result<GetRolesResponse>> GetRolesAsync()
    {
        await base.AddAuthorizeHeader();
        List<GetRolesResponse>? responseMessage = await HttpClient.GetFromJsonAsync<List<GetRolesResponse>>(Endpoints.Identity.GetRoles);
        return Result<GetRolesResponse>.Success(values: responseMessage!);
    }

    public async Task<Result> AssignRoleAsync(AssignRoleRequest assignRole)
    {
        await base.AddAuthorizeHeader();
        var response = await HttpClient.PutAsJsonAsync<AssignRoleRequest>(Endpoints.Identity.AssignRole, assignRole);

        if (response.IsSuccessStatusCode)
        {
            return Result.Success(message: "Role has been assigned the user.");
        }

        var message = await response.Content.ReadFromJsonAsync<Result>();
        return Result.Failure(error: message!.Message);
    }


}
