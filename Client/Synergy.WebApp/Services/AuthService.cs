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
        var hasHeader = await base.AddAuthorizeHeader();

        if (hasHeader)
        {
            var responseMessage = await HttpClient.GetAsync(Endpoints.Identity.GetUsers);
            List<GetUsersResponse>? response = await responseMessage.Content.ReadFromJsonAsync<List<GetUsersResponse>>();
            return Result<GetUsersResponse>.Success(response!);
        }

        return Result<GetUsersResponse>.Failure(error: "Server error");
    }

    public async Task<Result<GetRolesResponse>> GetRolesAsync()
    {
        var hasHeader = await base.AddAuthorizeHeader();
        if (hasHeader)
        {
            List<GetRolesResponse>? responseMessage = await HttpClient.GetFromJsonAsync<List<GetRolesResponse>>(Endpoints.Identity.GetRoles);
            return Result<GetRolesResponse>.Success(values: responseMessage!);
        }

        return Result<GetRolesResponse>.Failure(error: "You must be login!");
    }

    public async Task<Result> UpdateRoleAsync(UpdateRoleRequest updateRole)
    {
        var hasHeader = await base.AddAuthorizeHeader();

        if (hasHeader)
        {
            await HttpClient.PutAsJsonAsync<UpdateRoleRequest>(Endpoints.Identity.UpdateRole, updateRole);
            return Result.Success(message: "Role was updated successfully.");
        }

        return Result.Failure(error: "You must be login!");
    }

    public async Task<Result> CreateRoleAsync(CreateRoleRequest createRole)
    {
        var hasHeader = await base.AddAuthorizeHeader();
        if (hasHeader)
        {
            await HttpClient.PostAsJsonAsync<CreateRoleRequest>(Endpoints.Identity.CreateRole, createRole);
            return Result.Success(message: "Role was created successfully.");
        }

        return Result.Failure(error: "You must be login!");
    }

    public async Task<Result> AssignRoleAsync(AssignRoleRequest assignRole)
    {
        var hasHeader = await base.AddAuthorizeHeader();

        if (hasHeader)
        {
            var response = await HttpClient.PutAsJsonAsync<AssignRoleRequest>(Endpoints.Identity.AssignRole, assignRole);

            if (response.IsSuccessStatusCode)
            {
                return Result.Success(message: "Role has been assigned the user.");
            }

            var message = await response.Content.ReadFromJsonAsync<Result>();
            return Result.Failure(error: message!.Message);

        }

        return Result.Failure(error: "You must be login!");
    }


}
