using Microsoft.AspNetCore.Authentication;
using Synergy.Web.Constraints;
using System.Net.Http.Headers;

namespace Synergy.Web.Services;

public abstract class ClientServiceBase
{
    protected HttpClient HttpClient { get; }
    protected Endpoints Endpoints { get; }
    protected IHttpContextAccessor HttpContextAccessor { get; }
    public ClientServiceBase(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor)
    {
        HttpClient = clientFactory.CreateClient("SynergyWeb");
        Endpoints = endpoints;
        HttpContextAccessor = httpContextAccessor;

    }

    protected async Task<bool> AddAuthorizeHeaderAsync()
    {
        string? accessToken = await HttpContextAccessor.HttpContext!.GetTokenAsync(CookieConst.ACCESS_TOKEN);

        if (string.IsNullOrEmpty(accessToken))
            return false;

        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        return true;
    }

}
