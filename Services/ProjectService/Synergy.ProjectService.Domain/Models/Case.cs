using Synergy.ProjectService.Domain.Models.Abstracts;
using Synergy.ProjectService.Domain.Models.Enums;

namespace Synergy.ProjectService.Domain.Models;

public class Case : AuditableEntity
{
    public string Title { get; set; }
    public string? MemberId { get; set; }
    public string Description { get; set; }
    public Status CaseStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string ProjectId { get; set; }
    public Project? Project { get; set; }

    public ICollection<Comment>Comments { get; set; } = new HashSet<Comment>();

}
