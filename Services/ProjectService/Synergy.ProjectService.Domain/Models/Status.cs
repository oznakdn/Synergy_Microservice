using Synergy.ProjectService.Domain.Models.Abstracts;

namespace Synergy.ProjectService.Domain.Models;

public class Status : Entity
{
    public string Name { get; set; }
    public string ProjectId { get; set; }
    public Project? Project { get; set; }

    public ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();

}
