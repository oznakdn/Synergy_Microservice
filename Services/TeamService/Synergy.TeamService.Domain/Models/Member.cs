using Synergy.TeamService.Domain.Models.Abstracts;

namespace Synergy.TeamService.Domain.Models;

public class Member : AuditableEntity
{
    public string GivenName { get; set; }
    public string LastName { get; set; }
    public string Photo { get; set; }
    public string Title { get; set; }
    public Contact Contact { get; set; }
    public ICollection<Skill>Skills { get; set; } = new HashSet<Skill>();
    public Guid? TeamId { get; set; }

}
