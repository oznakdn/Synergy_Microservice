using Synergy.ProjectService.Infrastructure.Context;
using Synergy.ProjectService.Infrastructure.Repositories.Contracts;

namespace Synergy.ProjectService.Infrastructure.Repositories.Implementations;

public class RepositoryManager : IRepositoryManager
{
    private readonly IProjectRepo _project;
    private readonly ICaseRepo _case;
    private readonly AppDbContext _context;

    public RepositoryManager(IProjectRepo project, ICaseRepo @case, AppDbContext context)
    {
        _project = project;
        _case = @case;
        _context = context;
    }

    public IProjectRepo Project => _project;

    public ICaseRepo Case => _case;

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

}
