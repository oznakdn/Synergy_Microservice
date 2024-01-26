namespace Synergy.IdentityService.Shared.Dtos.UserDtos;

public record TokenDto(string Token, string TokenExpire, UserDto User);

