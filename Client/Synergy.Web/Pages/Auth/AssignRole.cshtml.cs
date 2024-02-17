using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Synergy.Web.Filters;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;


[ClientAuthenticationFilter]
public class AssignRoleModel(AuthService authService) : PageModel
{

    [BindProperty]
    public AssignRoleInput AssignRole { get; set; } = new();
    public SelectList RoleList { get; set; }
    public List<GetRolesOutput> Roles { get; set; } = new();

    public async Task OnGetAsync(string userId)
    {
        var roles = await authService.GetRolesAsync();
        var users = await authService.GetUsersAsync();
        var user = users.Values!.SingleOrDefault(x => x.Id == userId);
        AssignRole.UserId = userId;
        Roles = roles.Values!.ToList();

        if (!string.IsNullOrEmpty(user!.Role))
        {
            RoleList = new SelectList(Roles, "Id", "RoleName", roles.Values!.First(x => x.RoleName == user!.Role).Id);
        }
        else
        {
            RoleList = new SelectList(Roles, "Id", "RoleName");
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await authService.AssignRoleAsync(AssignRole);
        if (result.IsSuccess)
        {
            TempData["AssignRoleSuccess"] = result.Message;
            return RedirectToPage("/Auth/GetUsers");
        }

        ViewData["AssignRoleFailure"] = result.Message;
        return Page();
    }
}




