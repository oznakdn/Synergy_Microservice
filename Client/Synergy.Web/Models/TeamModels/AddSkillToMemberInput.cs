namespace Synergy.Web.Models.TeamModels;

public record AddSkillToMemberInput
{
    public string TechnologyId { get; init; }
    public string Experience { get; init; }
    public string DeveloperId { get; set; }
}
