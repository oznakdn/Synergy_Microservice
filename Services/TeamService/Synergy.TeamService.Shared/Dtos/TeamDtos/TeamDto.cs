namespace Synergy.TeamService.Shared.Dtos.TeamDtos;

public record TeamDto(string Id, string Name, string Description);

//public record TeamDevelopers(string Id, string GivenName, string LastName, string Title, string Photo, DeveloperContact Contact, List<DeveloperSkill>Skills);
//public record DeveloperContact(string Email, string PhoneNumber, string Address);
//public record DeveloperSkill(string Technology, string Experience);
