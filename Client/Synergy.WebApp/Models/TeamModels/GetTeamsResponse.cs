namespace Synergy.WebApp.Models.TeamModels;

public record GetTeamsResponse(string Id, string Name, string Description);

public record GetTeamDeveloper(string GivenName, string LastName, string Title);