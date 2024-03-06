using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Synergy.Web.Models.AuthModels;
using Synergy.Web.Services;

namespace Synergy.Web.Pages.Auth;

public class RegisterModel(AuthService authService, INotyfService notyf) : PageModel
{
    [BindProperty]
    public RegisterInput RegisterInput { get; set; }

    public SelectList Titles { get; set; }

    public void OnGet()
    {
        Titles = new SelectList(new List<string>
        {
           "Software Developer",
           "Back End Developer",
           "Front End Developer",
           "Software Engineer",
           "Project Manager",
           "Team Lead",
           "UI/UX Designer"
        });
    }

    public async Task<IActionResult> OnPost()
    {
        var response = await authService.RegisterAsync(RegisterInput);
        if (!response.IsSuccess)
        {
            notyf.Error("Register could not be!");
            return Page();
        }

        notyf.Success("Registered was be successfully.");
        return RedirectToPage("/Auth/Login", new { Username = RegisterInput.CreateUser.Username});
    }
}
