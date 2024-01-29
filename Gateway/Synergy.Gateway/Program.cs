using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot();
builder.Configuration.AddJsonFile("C:/Users/W10/OneDrive/Desktop/Synergy_Microservice/Gateway/Synergy.Gateway/ocelot.json", true, true);

var app = builder.Build();


app.UseAuthorization();

app.UseOcelot().Wait();

app.Run();
