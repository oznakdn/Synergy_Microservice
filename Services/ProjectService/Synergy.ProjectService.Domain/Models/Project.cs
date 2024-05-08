using Synergy.ProjectService.Domain.Models.Abstracts;

namespace Synergy.ProjectService.Domain.Models;

public class Project : AuditableEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? Github { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string TeamId { get; set; } // TeamService'den gelecek

    public ICollection<Status>Statuses { get; set; } = new HashSet<Status>();

}
