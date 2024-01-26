namespace Synergy.TeamService.Infrastructure.Repositories.Contracts;

public interface IRepositoryManager : IAsyncDisposable
{
    Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
    ITeamRepo Team { get; }
    IDeveloperRepo Developer { get; }
    ITechnologyRepo Technology { get; }
    IContactRepo Contact { get; }
    IDeveloperSkillRepo DeveloperSkill { get; }

}
