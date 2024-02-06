using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Models.AuthModels;
using Synergy.WebApp.Services;
using Synergy.WebApp.Filters;

namespace Synergy.WebApp.Pages.Auth;


[ClientAuthenticationFilter]
public class GetUsersModel(AuthService authService) : PageModel
{
    public List<GetUsersResponse> Users { get; set; } = new();

    public async Task OnGet()
    {
        var response = await authService.GetUsersAsync();
        Users = response.Values!.ToList();
    }
}
