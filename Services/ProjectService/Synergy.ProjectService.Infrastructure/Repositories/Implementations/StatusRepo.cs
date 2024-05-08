using Synergy.ProjectService.Domain.Models;
using Synergy.ProjectService.Infrastructure.Context;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;

namespace Synergy.ProjectService.Infrastructure.Repositories.Implementations;

public class StatusRepo : GenericRepository<Status>, IStatusRepository
{
    public StatusRepo(AppDbContext db) : base(db)
    {
    }
}
