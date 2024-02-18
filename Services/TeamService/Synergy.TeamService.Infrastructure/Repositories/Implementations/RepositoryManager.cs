using Synergy.TeamService.Infrastructure.Context;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class RepositoryManager : IRepositoryManager
{

    private readonly ITeamRepo _team;
    private readonly IMemberRepo _member;
    private readonly ITechnologyRepo _technology;
    private readonly IContactRepo _contact;
    private readonly ISkillRepo _skill;
    private readonly AppDbContext _context;

    public RepositoryManager(ITeamRepo team, IMemberRepo member, AppDbContext context, ITechnologyRepo technology, IContactRepo contact, ISkillRepo skill)
    {
        _team = team;
        _member = member;
        _technology = technology;
        _context = context;
        _contact = contact;
        _skill = skill;
    }

    public ITeamRepo Team => _team;

    public IMemberRepo Member => _member;

    public ITechnologyRepo Technology => _technology;

    public IContactRepo Contact => _contact;

    public ISkillRepo Skill => _skill;

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

}
