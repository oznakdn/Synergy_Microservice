using Synergy.Shared.Results;
using Synergy.Web.Models.TechnologyModels;

namespace Synergy.Web.Services;

public class TechnologyService : ClientServiceBase
{
    public TechnologyService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints, httpContextAccessor)
    {
    }


    public async Task<Result<GetTechnologiesOutput>> GetTechnologiesAsync()
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();
        if (hasHeader)
        {
            var response = await HttpClient.GetAsync(Endpoints.Team.GetTechnologies);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<GetTechnologiesOutput>>();
                return Result<GetTechnologiesOutput>.Success(values:result!);
            }

            return Result<GetTechnologiesOutput>.Failure();
        }

        return Result<GetTechnologiesOutput>.Failure(error: "You must be login!");
    }

    public async Task<Result> CreateTechnologyAsync(CreateTechnologyInput createTechnology)
    {

        var hasHeader = await base.AddAuthorizeHeaderAsync();
        if (hasHeader)
        {
            var response = await HttpClient.PostAsJsonAsync(Endpoints.Team.CreateTechnology, createTechnology);

            if (response.IsSuccessStatusCode)
            {
                return Result.Success();
            }

            return Result.Failure();
        }

        return Result.Failure(error: "You must be login!");

    }


}
