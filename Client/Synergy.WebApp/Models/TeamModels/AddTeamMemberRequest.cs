namespace Synergy.WebApp.Models.TeamModels;

public record AddTeamMemberRequest
{
    public string GivenName { get; init; }
    public string LastName { get; init; }
    public string Photo { get; set; }
    public string Title { get; init; }
    public string TeamId { get; set; }
    public MemberContact ContractDto { get; init; }
}

public record MemberContact(string Email, string PhoneNumber, string Address);