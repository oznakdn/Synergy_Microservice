namespace Synergy.TeamService.Shared.Dtos.DeveloperDtos;

public record CreateDeveloperDto(string GivenName, string LastName, string Photo, string Title, string TeamId, DeveloperContractDto ContractDto);

public record DeveloperContractDto(string Email, string PhoneNumber,string Address);