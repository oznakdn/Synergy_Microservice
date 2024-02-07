using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Models.AuthModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Auth;

public class CreateRoleModel(AuthService authService) : PageModel
{

    [BindProperty]
    public CreateRoleRequest CreateRole { get; set; }
 
    public async Task<IActionResult>OnPostAsync()
    {
        var result = await authService.CreateRoleAsync(CreateRole);
        if(result.IsSuccess)
        {
            return RedirectToPage("/Auth/GetRoles");
        }

        return Page();
    }
}
