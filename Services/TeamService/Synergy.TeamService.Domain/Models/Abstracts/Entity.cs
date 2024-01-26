
namespace Synergy.TeamService.Domain.Models.Abstracts;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
}
