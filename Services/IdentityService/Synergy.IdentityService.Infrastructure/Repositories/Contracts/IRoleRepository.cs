using Synergy.IdentityService.Domain.Models;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Infrastructure.Repositories.Contracts;

public interface IRoleRepository
{
    Task CreateAsync(Role role, CancellationToken cancellationToken = default(CancellationToken));
    Task Update(Role role, CancellationToken cancellationToken = default(CancellationToken));
    Task<Role> GetAsync(Expression<Func<Role, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
    Task<IList<Role>> GetAllAsync(Expression<Func<Role, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken));
}
