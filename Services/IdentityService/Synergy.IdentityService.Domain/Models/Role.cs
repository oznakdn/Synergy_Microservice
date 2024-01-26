using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Synergy.IdentityService.Domain.Models;

public class Role
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string RoleName { get; set; }
    public string Description { get;set; }

}
