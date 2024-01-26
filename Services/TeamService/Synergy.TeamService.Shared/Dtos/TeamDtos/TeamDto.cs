namespace Synergy.TeamService.Shared.Dtos.TeamDtos;

public record TeamDto(string Id, string Name, string Description, List<TeamDevelopers> Developers);

public record TeamDevelopers(string GivenName, string LastName, string Title);