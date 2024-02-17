using StackExchange.Redis;
using Synergy.TeamService.Domain.Models;
using Synergy.TeamService.Infrastructure.Repositories.Contracts;
using System.Text.Json;

namespace Synergy.TeamService.Infrastructure.Repositories.Implementations;

public class TechnologyRepo : ITechnologyRepo
{
    private readonly IConnectionMultiplexer _redis;
    public TechnologyRepo(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task InsertTechnologyAsync(Technology technology)
    {
        var db = _redis.GetDatabase();
        technology.Id = Guid.NewGuid();
        string stringTechnology = JsonSerializer.Serialize(technology);
        await db.StringSetAsync(technology.Id.ToString(), stringTechnology);

        await db.SetAddAsync("Technologies", stringTechnology);
    }

    public async Task<IEnumerable<Technology>> GetTechnologies()
    {
        var db = _redis.GetDatabase();
        var completeSet = await db.SetMembersAsync("Technologies");

        if (completeSet.Length > 0)
        {
            var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<Technology>(val)).ToList();
            return obj!;
        }

        return Enumerable.Empty<Technology>();
    }

    public async Task<Technology> GetTechnology(string id)
    {
        var db = _redis.GetDatabase();

        string? stringTechnology = await db.StringGetAsync(id);

        if (string.IsNullOrEmpty(stringTechnology))
            return default(Technology)!;

        Technology technology = JsonSerializer.Deserialize<Technology>(stringTechnology)!;
        return technology;
    }


    public async Task<bool> RemoveTechnology(string id)
    {
        var db = _redis.GetDatabase();

        string? stringTechnology = await db.StringGetAsync(id);
        if (string.IsNullOrEmpty(stringTechnology))
        {
            return false;
        }

        await db.GeoRemoveAsync(id, stringTechnology);
        return true;

    }




}
