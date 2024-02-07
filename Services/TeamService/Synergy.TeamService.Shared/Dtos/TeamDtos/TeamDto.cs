namespace Synergy.TeamService.Shared.Dtos.TeamDtos;

public record TeamDto(string Id, string Name, string Description);

public record TeamDevelopers(string GivenName, string LastName, string Title);