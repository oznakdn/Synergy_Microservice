namespace Synergy.TeamService.Shared.Dtos.DeveloperDtos;

public record CreateDeveloperDto(string GivenName, string LastName, string Photo, string Title, string TeamId, DeveloperContact ContractDto);
