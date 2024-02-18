using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class MemberRepo : GenericRepository<Member>, IMemberRepo
{
    public MemberRepo(AppDbContext db) : base(db)
    {
    }
}
