using Synergy.ProjectService.Domain.Models.Abstracts;

namespace Synergy.ProjectService.Domain.Models;

public class Comment : AuditableEntity
{
    public string Title { get; set;}
    public string Text { get; set; }
    public string CaseId { get; set; }
    public Case? Case { get; set; }
}
