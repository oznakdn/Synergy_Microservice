using Synergy.TeamService.Domain.Models.Abstracts;

namespace Synergy.TeamService.Domain.Models;

public class DeveloperSkill : AuditableEntity
{
    public Guid TechnologyId { get; set; }
    public Guid DeveloperId { get; set; }
    public Technology? Technology { get; set;}
    public Developer? Developer { get; set; }
    public string Experience { get; set; }

}
