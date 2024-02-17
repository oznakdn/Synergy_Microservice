using Synergy.TeamService.Domain.Models;

namespace Synergy.TeamService.Infrastructure.Repositories.Contracts;

public interface ITechnologyRepo
{
    Task InsertTechnologyAsync(Technology technology);
    Task<IEnumerable<Technology>> GetTechnologies();
    Task<Technology> GetTechnology(string id);
    Task<bool> RemoveTechnology(string id);

}
