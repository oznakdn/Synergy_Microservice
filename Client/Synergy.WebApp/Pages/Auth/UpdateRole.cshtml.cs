using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.WebApp.Filters;
using Synergy.WebApp.Models.AuthModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Auth;


[ClientAuthenticationFilter]
public class UpdateRoleModel(AuthService authService) : PageModel
{


    [BindProperty]
    public UpdateRoleRequest UpdateRole { get; set; } = new();

    public async Task OnGetAsync(string roleId)
    {
        var roles = await authService.GetRolesAsync();
        var role = roles.Values!.Where(_ => _.Id == roleId).FirstOrDefault();
        UpdateRole.RoleName = role!.RoleName;
        UpdateRole.Description = role.Description;
        UpdateRole.Id = roleId;
    }

    public async Task<IActionResult> OnPostEditAsync()
    {
        var result = await authService.UpdateRoleAsync(UpdateRole);
        if (result.IsSuccess)
        {
            return RedirectToPage("/Auth/GetRoles");
        }

        return Page();
    }
}
