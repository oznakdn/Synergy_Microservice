using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Synergy.WebApp.Services;

public abstract class ClientServiceBase
{
    protected HttpClient HttpClient { get; }
    protected Endpoints Endpoints { get; }
    protected IHttpContextAccessor HttpContextAccessor { get; }
    public ClientServiceBase(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor)
    {
        HttpClient = clientFactory.CreateClient("SynergyClient");
        Endpoints = endpoints;
        HttpContextAccessor = httpContextAccessor;

    }

    protected async Task AddAuthorizeHeader()
    {
        string? accessToken = await HttpContextAccessor.HttpContext!.GetTokenAsync("access_token");
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

}
