namespace Synergy.IdentityService.Shared.Dtos.UserDtos;

public record UserDto(string Id, string Username, string Email, string MemberId, string? Role = null);
