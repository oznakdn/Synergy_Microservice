namespace Synergy.TeamService.Infrastructure.Repositories.Contracts;

public interface IRepositoryManager : IAsyncDisposable
{
    Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    ITeamRepo Team { get; }
    IMemberRepo Member { get; }
    ITechnologyRepo Technology { get; }
    IContactRepo Contact { get; }
    ISkillRepo Skill { get; }

}
