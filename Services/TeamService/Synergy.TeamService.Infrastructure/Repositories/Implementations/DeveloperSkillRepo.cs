using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class DeveloperSkillRepo : GenericRepository<DeveloperSkill>, IDeveloperSkillRepo
{
    public DeveloperSkillRepo(AppDbContext db) : base(db)
    {
    }
}
