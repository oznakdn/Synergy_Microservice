namespace Synergy.ProjectService.Domain.Models.Abstracts;

public abstract class Entity : IEntity
{
    public string Id { get; set; }
    public Entity()
    {
        Id = Guid.NewGuid().ToString();
    }
}
