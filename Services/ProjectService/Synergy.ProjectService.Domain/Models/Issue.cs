using Synergy.ProjectService.Domain.Models.Abstracts;
using Synergy.ProjectService.Domain.Models.Enums;

namespace Synergy.ProjectService.Domain.Models;

public class Issue : AuditableEntity
{
    public string Summary { get; set; }
    public string? Description { get; set; }
    public string? MemberId { get; set; }
    public PriorityType PriorityType { get; set; }
    public IssueType IssueType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string StatusId { get; set; }
    public Status? Status { get; set; }
    public int Index { get; set; }

    public ICollection<Comment>Comments { get; set; } = new HashSet<Comment>();

}
