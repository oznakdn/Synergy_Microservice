using Synergy.ProjectService.Domain.Models;

namespace Synergy.ProjectService.Infrastructure.Repositories.Contracts;

public interface IIssueRepository : IGenericRepository<Issue>
{
    Task<int> SetIssueIndex(string statusId);
}
