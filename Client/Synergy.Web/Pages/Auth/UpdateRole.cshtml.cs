using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Synergy.Web.Filters;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;

[ClientAuthenticationFilter]
public class UpdateRoleModel(AuthService authService) : PageModel
{
    [BindProperty]
    public UpdateRoleInput UpdateRole { get; set; } = new();
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
            TempData["RoleUpdateSuccess"] = "Role has been updated successfully";
            return RedirectToPage("/Auth/GetRoles");
        }

        ViewData["RoleUpdateFailure"] = result.Message;
        return Page();
    }
}
