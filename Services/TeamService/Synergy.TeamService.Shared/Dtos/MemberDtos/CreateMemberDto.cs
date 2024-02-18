namespace Synergy.TeamService.Shared.Dtos.MemberDtos;

public record CreateMemberDto(string GivenName, string LastName, string Photo, string Title,MemberContact ContractDto, CreateUserDto CreateUser);
public record CreateUserDto(string Username, string Email, string Password);
