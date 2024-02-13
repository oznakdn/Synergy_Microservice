namespace Synergy.WebApp.Models.TeamModels;

public record GetDeveloperDetailsResponse(GetDevelopersResponse Developer, DeveloperContact Contact, List<DeveloperSkill> Skills);

public record DeveloperContact(string Email, string PhoneNumber, string Address);
public record DeveloperSkill(string Technology, string Experience);