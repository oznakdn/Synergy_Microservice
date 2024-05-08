using Microsoft.EntityFrameworkCore;
using Synergy.ProjectService.Domain.Models;
using Synergy.ProjectService.Infrastructure.Context;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;

namespace Synergy.ProjectService.Infrastructure.Repositories.Implementations;

public class IssueRepo : GenericRepository<Issue>, IIssueRepository
{
    public IssueRepo(AppDbContext db) : base(db)
    {
    }

    public async Task<int> SetIssueIndex(string statusId)
    {
        int index = 0;
        var issues = await _db.Issues.Where(x => x.StatusId == statusId).Include(x => x.Status).ToListAsync();

        if (issues.Count > 0)
        {
            int lastIndex = issues.Max(x => x.Index);
            return index = lastIndex++;
        }
        return index;
    }
}
