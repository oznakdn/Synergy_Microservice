namespace Synergy.Web.Models.TeamModels;

public record GetMemberOutput(string Id, string GivenName, string LastName, string Photo, string Title, string? TeamId);

