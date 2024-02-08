using Synergy.Shared.Results;
using Synergy.WebApp.Models.TechnologyModels;

namespace Synergy.WebApp.Services;

public class TechnologyService : ClientServiceBase
{
    public TechnologyService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints, httpContextAccessor)
    {
    }


    public async Task<Result<GetTechnologiesResponse>> GetTechnologiesAsync()
    {
        var hasHeader = await base.AddAuthorizeHeader();
        if (hasHeader)
        {
            var response = await HttpClient.GetAsync(Endpoints.Team.GetTechnologies);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<GetTechnologiesResponse>>();
                return Result<GetTechnologiesResponse>.Success(values:result!);
            }

            return Result<GetTechnologiesResponse>.Failure();
        }

        return Result<GetTechnologiesResponse>.Failure(error: "You must be login!");
    }

    public async Task<Result>CreateTechnologyAsync(CreateTechnologyRequest createTechnology)
    {

        var hasHeader = await base.AddAuthorizeHeader();
        if(hasHeader)
        {
            var response = await HttpClient.PostAsJsonAsync<CreateTechnologyRequest>(Endpoints.Team.CreateTechnology, createTechnology);

            if(response.IsSuccessStatusCode)
            {
                return Result.Success();
            }

            return Result.Failure();
        }

        return Result.Failure(error: "You must be login!");

    }


}
