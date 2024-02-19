namespace Synergy.Web.Models.TeamModels;

public record GetMemberDetailOutput(GetMemberOutput Member, MemberContact Contact, List<MemberSkill> Skills);

public record MemberContact(string PhoneNumber, string Address);
public record MemberSkill(string Technology, string Experience);
