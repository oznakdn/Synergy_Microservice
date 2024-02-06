using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Synergy.WebApp.Models.AuthModels;
using Synergy.WebApp.Services;

namespace Synergy.WebApp.Pages.Auth;

public class AssignRoleModel(AuthService authService) : PageModel
{

    [BindProperty]
    public AssignRoleRequest AssignRole { get; set; } = new();
    public SelectList RoleList { get; set; }
    public List<GetRolesResponse> Roles { get; set; } = new();

    public async Task OnGet(string userId)
    {
        var roles = await authService.GetRolesAsync();
        AssignRole.UserId = userId;
        Roles = roles.Values!.ToList();
        RoleList = new SelectList(Roles, "Id", "RoleName");
    }

    public async Task<IActionResult> OnPost(string roleId)
    {
        AssignRole.RoleId = roleId;
        var result = await authService.AssignRoleAsync(AssignRole);
        if(result.IsSuccess)
        {
            return RedirectToPage("/Auth/GetUsers");
        }

        ViewData["error"] = result.Message;
        return Page();
    }

   

}
