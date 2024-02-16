namespace Synergy.WebApp.Models.TeamModels;

public record AddSkillToMemberRequest
{
    public string TechnologyId { get; init; }
    public string Experience { get; init ; }
    public string DeveloperId { get; set; }
}

