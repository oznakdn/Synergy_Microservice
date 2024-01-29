using Synergy.ProjectService.Domain.Models;
using Synergy.ProjectService.Infrastructure.Context;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;

namespace Synergy.ProjectService.Infrastructure.Repositories.Implementations;

public class ProjectRepo : GenericRepository<Project>, IProjectRepo
{
    public ProjectRepo(AppDbContext db) : base(db)
    {
    }
}
