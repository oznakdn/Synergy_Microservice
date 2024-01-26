namespace Synergy.IdentityService.Infrastructure.Options;

public interface IMongoOption
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}
