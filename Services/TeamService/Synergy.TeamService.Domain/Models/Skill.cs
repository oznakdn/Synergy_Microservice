using Synergy.TeamService.Domain.Models.Abstracts;

namespace Synergy.TeamService.Domain.Models;

public class Skill : AuditableEntity
{
    public Guid TechnologyId { get; set; }
    public Guid MemberId { get; set; }
    public Technology? Technology { get; set;}
    public Member? Member { get; set; }
    public string Experience { get; set; }

}
