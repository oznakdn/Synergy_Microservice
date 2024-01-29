using Synergy.ProjectService.Domain.Models;
using Synergy.ProjectService.Infrastructure.Context;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;

namespace Synergy.ProjectService.Infrastructure.Repositories.Implementations;

public class CaseRepo : GenericRepository<Case>, ICaseRepo
{
    public CaseRepo(AppDbContext db) : base(db)
    {
    }
}
