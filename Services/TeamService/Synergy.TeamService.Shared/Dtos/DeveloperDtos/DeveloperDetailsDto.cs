namespace Synergy.TeamService.Shared.Dtos.DeveloperDtos;

public record DeveloperDetailsDto(DeveloperDto Developer, DeveloperContact Contact, List<DeveloperSkill>Skills);
public record DeveloperContact(string Email, string PhoneNumber, string Address);
public record DeveloperSkill(string Technology, string Experience);