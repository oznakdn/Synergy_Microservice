using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;

public class GetUserByMemberModel(AuthService authService) : PageModel
{
    public GetUsersOutput User { get; set; }
    public async Task OnGetAsync(string memberId)
    {
        var result = await authService.GetUserByMemberIdAsync(memberId);
        if (result.IsSuccess)
        {
            User = result.Value!;
        }
    }
}
