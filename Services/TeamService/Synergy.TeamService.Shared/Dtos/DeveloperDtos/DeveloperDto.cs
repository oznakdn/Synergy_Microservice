namespace Synergy.TeamService.Shared.Dtos.DeveloperDtos;

public record DeveloperDto(string GivenName, string LastName, string Photo, string Title, string Team, List<DeveloperSkillDto> DeveloperSkills);

public record DeveloperSkillDto(string Technology, string Experience);
