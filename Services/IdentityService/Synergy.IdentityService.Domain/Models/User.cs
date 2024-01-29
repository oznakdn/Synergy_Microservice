using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Synergy.IdentityService.Domain.Models;

public class User
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Token { get; set; } 
    public DateTime? TokenExpire { get; set; }

    public Role? Role { get; set; }

}
