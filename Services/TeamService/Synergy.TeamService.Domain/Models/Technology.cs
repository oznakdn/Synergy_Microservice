using Synergy.TeamService.Domain.Models.Abstracts;

namespace Synergy.TeamService.Domain.Models;

public class Technology : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
