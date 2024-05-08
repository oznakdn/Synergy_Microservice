namespace Synergy.ProjectService.Infrastructure.Repositories.Contracts;

public interface IRepositoryManager : IAsyncDisposable
{
    Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    IProjectRepository Project { get; }
    IStatusRepository Status { get; }
    IIssueRepository Issue { get; }
    ICommentRepository Comment { get; }

}
