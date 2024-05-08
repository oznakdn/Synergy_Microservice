using Synergy.ProjectService.Infrastructure.Context;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;

namespace Synergy.ProjectService.Infrastructure.Repositories.Implementations;

public class RepositoryManager : IRepositoryManager
{
    private readonly IProjectRepository _project;
    private readonly IStatusRepository _status;
    private readonly IIssueRepository _issue;
    private readonly ICommentRepository _comment;

    private readonly AppDbContext _context;

    public RepositoryManager(IProjectRepository project, IIssueRepository issue, AppDbContext context, IStatusRepository status, ICommentRepository comment)
    {
        _project = project;
        _issue = issue;
        _context = context;
        _status = status;
        _comment = comment;
    }

    public IProjectRepository Project => _project;

    public IIssueRepository Issue => _issue;

    public IStatusRepository Status => _status;

    public ICommentRepository Comment => _comment;

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

}
