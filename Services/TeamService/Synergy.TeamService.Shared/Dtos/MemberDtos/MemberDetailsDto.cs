namespace Synergy.TeamService.Shared.Dtos.MemberDtos;

public record MemberDetailsDto(MemberDto Developer, MemberContact Contact, List<MemberSkill>Skills);
public record MemberContact(string Email, string PhoneNumber, string Address);
public record MemberSkill(string Technology, string Experience);