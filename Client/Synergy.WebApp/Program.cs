using Microsoft.Extensions.Options;
using Synergy.WebApp;
using Synergy.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddHttpClient("SynergyClient", conf => conf.BaseAddress = new Uri(builder.Configuration["BaseUrl"]!));
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<Endpoints>(builder.Configuration.GetSection(nameof(Endpoints)));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<Endpoints>>().Value);

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();


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
