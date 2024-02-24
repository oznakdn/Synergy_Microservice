using MongoDB.Driver;
using Synergy.IdentityService.Domain.Models;
using Synergy.IdentityService.Infrastructure.Options;
using Synergy.IdentityService.Infrastructure.Repositories.Contracts;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Infrastructure.Repositories.Implementations;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(IMongoOption mongoOption) : base(mongoOption)
    {
    }

    public async Task CreateAsync(User user, CancellationToken cancellationToken = default) => await _collection.InsertOneAsync(user);



    public async Task<List<User>> GetAllAsync(Expression<Func<User, bool>> predicate = null, CancellationToken cancellationToken = default)
    {
        return predicate is null ? await _collection.Find(_ => true).ToListAsync(cancellationToken)
                                 : await _collection.Find(predicate).ToListAsync(cancellationToken);
    }

    public async Task<User> GetAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(predicate).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task Update(User user, CancellationToken cancellationToken = default)=> await _collection.FindOneAndReplaceAsync<User>(_ => _.Id.Equals(user.Id), user,cancellationToken:cancellationToken);
    
}
