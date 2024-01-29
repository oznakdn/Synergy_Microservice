using Synergy.ProjectService.Domain.Models.Abstracts;
using Synergy.ProjectService.Domain.Models.Enums;

namespace Synergy.ProjectService.Domain.Models;

public class Project : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Status ProjectStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string TeamId { get; set; } // TeamService'den gelecek

    public ICollection<Case>Cases { get; set; } = new HashSet<Case>();

}
