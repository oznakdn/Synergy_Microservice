using Synergy.TeamService.Domain.Models.Abstracts;

namespace Synergy.TeamService.Domain.Models;

public class Developer : AuditableEntity
{
    public string GivenName { get; set; }
    public string LastName { get; set; }
    public string Photo { get; set; }
    public string Title { get; set; }
    public Contact Contact { get; set; }
    public ICollection<DeveloperSkill>Skills { get; set; } = new HashSet<DeveloperSkill>();

    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }

}
