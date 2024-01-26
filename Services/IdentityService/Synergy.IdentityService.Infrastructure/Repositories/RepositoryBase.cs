using MongoDB.Driver;
using Synergy.IdentityService.Infrastructure.Options;
using System.Linq.Expressions;

namespace Synergy.IdentityService.Infrastructure.Repositories;

public abstract class RepositoryBase<TCollection>
{
    protected IMongoCollection<TCollection> _collection { get; }

    public RepositoryBase(IMongoOption mongoOption)
    {
        var client = new MongoClient(mongoOption.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoOption.DatabaseName);
        _collection = database.GetCollection<TCollection>(typeof(TCollection).Name);
    }

}
