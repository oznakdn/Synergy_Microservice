namespace Synergy.WebApp.Services;

public abstract class ClientServiceBase
{
    protected HttpClient HttpClient { get; }
    protected Endpoints Endpoints { get; }
    public ClientServiceBase(IHttpClientFactory clientFactory, Endpoints endpoints)
    {
        HttpClient = clientFactory.CreateClient("SynergyClient");
        Endpoints = endpoints;
    }

}
