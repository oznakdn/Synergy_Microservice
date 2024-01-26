using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class DeveloperRepo : GenericRepository<Developer>, IDeveloperRepo
{
    public DeveloperRepo(AppDbContext db) : base(db)
    {
    }
}
