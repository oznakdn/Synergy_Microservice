using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class TechnologyRepo : GenericRepository<Technology>, ITechnologyRepo
{
    public TechnologyRepo(AppDbContext db) : base(db)
    {
    }

    public void InsertRange(List<Technology> technologies) => _db.Technologies.AddRange(technologies);
}
