using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot();
builder.Configuration.AddJsonFile("ocelot.json", true, true);
builder.Services.AddCors(opt => opt.AddPolicy("Policy", policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.WithOrigins("http://localhost:5104");
    policy.AllowCredentials();
}));


var app = builder.Build();

app.UseCors("Policy");

app.UseAuthorization();

app.UseOcelot().Wait();

app.Run();
