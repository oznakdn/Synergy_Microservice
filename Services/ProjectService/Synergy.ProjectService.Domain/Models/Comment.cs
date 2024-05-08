using Synergy.ProjectService.Domain.Models.Abstracts;

namespace Synergy.ProjectService.Domain.Models;

public class Comment : AuditableEntity
{
    public string Text { get; set; }
    public string MemberId { get; set; }
    public string IssueId { get; set; }
    public Issue? Issue { get; set; }
}
