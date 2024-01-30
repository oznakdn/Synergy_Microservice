using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Models.AuthModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Auth;

public class GetUsersModel(AuthService authService) : PageModel
{
    public IEnumerable<GetUsersResponse> Users { get; set; }
    public async Task OnGet()
    {
        var response = await authService.GetUsersAsync();
        Users = response.Values!.ToList();
    }
}
