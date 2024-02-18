using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(scheme =>
{
    scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["TokenOption:Audience"],
        ValidIssuer = builder.Configuration["TokenOption:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenOption:Key"]!))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HasRole", policy =>
    policy.RequireAssertion(context =>
        context.User.HasClaim(c => c.Type == ClaimTypes.Role)));
});


builder.Services.AddGraphQLServer();



var app = builder.Build();


app.UseAuthentication();

app.UseAuthorization();

app.MapGraphQL();

app.Run();

