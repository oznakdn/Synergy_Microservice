using Synergy.TeamService.Domain.Models;

namespace Synergy.TeamService.Infrastructure.Repositories.Contracts;

public interface ITechnologyRepo : IGenericRepository<Technology>
{
    void InsertRange(List<Technology> technologies);
}
