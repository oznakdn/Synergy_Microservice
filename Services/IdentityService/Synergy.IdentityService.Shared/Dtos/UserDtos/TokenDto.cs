namespace Synergy.IdentityService.Shared.Dtos.UserDtos;

public record TokenDto(string Token, string TokenExpire, string RefreshToken, string RefreshExpire, UserDto? User = null, string? Role = null);

