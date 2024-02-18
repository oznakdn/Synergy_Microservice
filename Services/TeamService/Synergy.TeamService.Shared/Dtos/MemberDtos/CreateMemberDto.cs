namespace Synergy.TeamService.Shared.Dtos.MemberDtos;

public record CreateMemberDto(string GivenName, string LastName, string Photo, string Title, string TeamId, MemberContact ContractDto);
