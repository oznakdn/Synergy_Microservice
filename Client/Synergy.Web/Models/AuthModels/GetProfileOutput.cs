namespace Synergy.Web.Models.AuthModels;

public record GetProfileOutput(string Id, string Username, string Email, string? Role = null);
