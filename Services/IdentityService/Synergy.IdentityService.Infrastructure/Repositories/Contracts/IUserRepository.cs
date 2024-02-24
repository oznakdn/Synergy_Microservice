using Synergy.IdentityService.Domain.Models;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Infrastructure.Repositories.Contracts;

public interface IUserRepository
{
    Task CreateAsync(User user, CancellationToken cancellationToken = default(CancellationToken));
    Task Update(User user, CancellationToken cancellationToken = default(CancellationToken));
    Task<User> GetAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
    Task<List<User>> GetAllAsync(Expression<Func<User, bool>> predicate = null, CancellationToken cancellationToken = default(CancellationToken));
}
