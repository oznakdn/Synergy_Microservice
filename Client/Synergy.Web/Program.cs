using Microsoft.Extensions.Options;
using Synergy.Web;
using Synergy.Web.Filters;
using Synergy.Web.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();
        builder.Services.AddHttpClient("SynergyWeb", conf => conf.BaseAddress = new Uri(builder.Configuration["BaseUrl"]!));
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddAuthentication("Bearer")
                        .AddCookie("Bearer");

        builder.Services.Configure<Endpoints>(builder.Configuration.GetSection(nameof(Endpoints)));
        builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<Endpoints>>().Value);

        builder.Services.AddScoped<ClientAuthenticationFilter>();


        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<TechnologyService>();
        builder.Services.AddScoped<TeamService>();
        builder.Services.AddScoped<ProjectService>();



        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}