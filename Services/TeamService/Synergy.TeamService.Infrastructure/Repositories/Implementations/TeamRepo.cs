using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class TeamRepo : GenericRepository<Team>, ITeamRepo
{
    public TeamRepo(AppDbContext db) : base(db)
    {
    }
}
