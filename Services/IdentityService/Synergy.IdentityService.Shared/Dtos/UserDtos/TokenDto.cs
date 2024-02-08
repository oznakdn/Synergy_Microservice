namespace Synergy.IdentityService.Shared.Dtos.UserDtos;

public record TokenDto(string Token, DateTimeOffset TokenExpire, string RefreshToken, DateTimeOffset RefreshExpire, UserDto User);

