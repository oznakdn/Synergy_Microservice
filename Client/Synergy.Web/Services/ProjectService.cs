using Synergy.Shared.Results;
using Synergy.Web.Models.ProjectModels;

namespace Synergy.Web.Services;

public class ProjectService : ClientServiceBase
{
    public ProjectService(IHttpClientFactory clientFactory, Endpoints endpoints, IHttpContextAccessor httpContextAccessor) : base(clientFactory, endpoints, httpContextAccessor)
    {
    }



    public async Task<IResult<GetProjectsOutput>>GetProjectsAsync()
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();

        if (hasHeader)
        {
            var response = await HttpClient.GetAsync(Endpoints.Project.GetProjects);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<GetProjectsOutput>>();
                return Result<GetProjectsOutput>.Success(values:result!);
            }

            return Result<GetProjectsOutput>.Failure();
        }

        return Result<GetProjectsOutput>.Failure(error: "You must be login!");
    }

    public async Task<Shared.Results.IResult>CreateprojectAsync(CreateProjectInput createProject)
    {
        var hasHeader = await base.AddAuthorizeHeaderAsync();

        if (hasHeader)
        {
            var response = await HttpClient.PostAsJsonAsync(Endpoints.Project.CreateProject, createProject);

            if (response.IsSuccessStatusCode)
            {
                return Result.Success(message: "The project has been created successfully.");
            }

            return Result.Failure();
        }

        return Result.Failure(error: "You must be login!");
    }
}
