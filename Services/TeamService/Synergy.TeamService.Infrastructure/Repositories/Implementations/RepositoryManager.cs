using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class RepositoryManager : IRepositoryManager
{

    private readonly ITeamRepo _team;
    private readonly IDeveloperRepo _developer;
    private readonly ITechnologyRepo _technology;
    private readonly IContactRepo _contact;
    private readonly IDeveloperSkillRepo _developerSkill;
    private readonly AppDbContext _context;

    public RepositoryManager(ITeamRepo team, IDeveloperRepo developer, AppDbContext context, ITechnologyRepo technology, IContactRepo contact, IDeveloperSkillRepo developerSkill)
    {
        _team = team;
        _developer = developer;
        _technology = technology;
        _context = context;
        _contact = contact;
        _developerSkill = developerSkill;
    }

    public ITeamRepo Team => _team;

    public IDeveloperRepo Developer => _developer;

    public ITechnologyRepo Technology => _technology;

    public IContactRepo Contact => _contact;

    public IDeveloperSkillRepo DeveloperSkill => _developerSkill;

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

}
