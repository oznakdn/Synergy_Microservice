namespace Synergy.ProjectService.Infrastructure.Repositories.Contracts;

public interface IRepositoryManager : IAsyncDisposable
{
    Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    IProjectRepo Project { get; }
    ICaseRepo Case { get; }

}
