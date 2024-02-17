using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Filters;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;


[ClientAuthenticationFilter]
public class GetUsersModel(AuthService authService) : PageModel
{
    public List<GetUsersOutput> Users { get; set; } = new();
    public async Task OnGetAsync()
    {
        var result = await authService.GetUsersAsync();
        Users = result.Values!.ToList();
    }
}
