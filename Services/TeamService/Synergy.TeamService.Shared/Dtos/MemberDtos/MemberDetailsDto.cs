namespace Synergy.TeamService.Shared.Dtos.MemberDtos;

public record MemberDetailsDto(MemberDto Member, MemberContact Contact, List<MemberSkill>Skills);
public record MemberContact(string PhoneNumber, string Address);
public record MemberSkill(string Technology, string Experience);